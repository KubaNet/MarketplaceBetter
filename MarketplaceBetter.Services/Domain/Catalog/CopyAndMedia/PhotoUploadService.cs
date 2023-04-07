using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;
using Color = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Color;
using Size = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Size;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Entities.Base;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<PhotoUpload> _repository;
        private readonly IRepository<PhotoUploadVariant> _photoUploadVariantRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<PhotoKind> _photoKindRepository;
        private readonly IRepository<PhotoKindBeginning> _photoKindBeginningRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<Color> _colorRepository;
        private readonly IRepository<Size> _sizeRepository;
        private readonly IPhotoCloudService _photoCloudService;
        private readonly IPhotoService _photoService;

        public PhotoUploadService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPhotoCloudService photoCloudService,
            IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<PhotoUpload>();
            _photoUploadVariantRepository = unitOfWork.GetRepository<PhotoUploadVariant>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _photoKindRepository = unitOfWork.GetRepository<PhotoKind>();
            _photoKindBeginningRepository = unitOfWork.GetRepository<PhotoKindBeginning>();
            _brandRepository = unitOfWork.GetRepository<Brand>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _colorRepository = unitOfWork.GetRepository<Color>();
            _sizeRepository = unitOfWork.GetRepository<Size>();
            _photoCloudService = photoCloudService;
            _photoService = photoService;
        }

        public IList<PhotoUploadModel> GetForListRequest(ListRequest request)
        {
            IQueryable<PhotoUpload> photos = _repository.GetQuery();

            photos = ApplyFilter(photos, request);
            photos = ApplySorting(photos, request);

            return _mapper.Map<IList<PhotoUploadModel>>(photos);
        }

        public void AddOrUpdate(PhotoUploadModel photoUpload)
        {
            if (_repository.Any(p => p.CloudId == photoUpload.CloudId))
            {
                Update(photoUpload);
            }
            else
            {
                Add(photoUpload);
            }
        }

        public void Remove(PhotoUploadModel photoUpload)
        {
            _photoCloudService.Delete(photoUpload.CloudId);

            PhotoUpload photoUploadToDelete = _repository.Get(photoUpload.Id);
            _repository.Delete(photoUploadToDelete);
            _unitOfWork.Save();
        }

        public void Remove(IList<PhotoUploadModel> photoUploads)
        {
            _photoCloudService.Delete(photoUploads.Select(p => p.CloudId).ToList());

            foreach (var photoUpload in photoUploads)
            {
                PhotoUpload photoUploadToDelete = _repository.Get(photoUpload.Id);
                _repository.Delete(photoUploadToDelete);
            }

            _unitOfWork.Save();
        }

        public void RemoveAllForVariant(long  variantId)
        {
            IList<PhotoUploadVariant> photoUploadsVariants = _photoUploadVariantRepository.Where(p => p.VariantId == variantId).ToList();

            foreach (var photoUploadVariant in photoUploadsVariants)
            {
                _photoUploadVariantRepository.Delete(photoUploadVariant);
                _unitOfWork.Save();

                PhotoUpload photoUpload = _repository.Single(p => p.Id == photoUploadVariant.PhotoUploadId);
                if (!photoUpload.Variants.Any())
                {
                    Remove(_mapper.Map<PhotoUploadModel>(photoUpload));
                }
            }
        }

        public void SetType(IList<PhotoUploadModel> photoUploads, PhotoTypeModel type)
        {
            foreach (var photoUpload in photoUploads)
            {
                PhotoUpload photoUploadToUpdate = _repository.Get(photoUpload.Id);

                photoUploadToUpdate.TypeId = type.Id;

                _repository.Update(photoUploadToUpdate);
            }

            _unitOfWork.Save();
        }

        public void Save(PhotoUploadModel photoUpload, VariantModel variant)
        {
            string fileName = $"{photoUpload.Instance.ShortName.ToLower()}_variant_{variant.Id}_{photoUpload.Type.SystemName}";
            string fullFileName = $"product_{variant.Product.Id}/variant_{variant.Id}/instance_{photoUpload.Instance.ShortName.ToLower()}/{photoUpload.Instance.ShortName.ToLower()}_variant_{variant.Id}_{photoUpload.Type.SystemName}";
            
            PhotoUploadResult result = _photoCloudService.SaveFromPreUpload(photoUpload.CloudId, photoUpload.Version, fullFileName);

            PhotoModel photo = new PhotoModel();

            TransferValues(photo, photoUpload, result, fileName);

            _photoService.AddOrUpdate(photo, variant);
        }

        private void TransferValues(PhotoModel toPhoto, PhotoUploadModel fromPhoto, PhotoUploadResult fromResult, string fileName)
        {
            toPhoto.Instance = fromPhoto.Instance;
            toPhoto.Type = fromPhoto.Type;
            toPhoto.Kind = fromPhoto.Kind;
            toPhoto.Height = fromPhoto.Height;
            toPhoto.Width = fromPhoto.Width;
            toPhoto.CloudId = fromResult.CloudId;
            toPhoto.Version = fromResult.Version;
            toPhoto.Url = fromResult.Url;
            toPhoto.FileName = fileName;
        }

        private void Add(PhotoUploadModel photoUpload)
        {
            PhotoUpload photoUploadToAdd = new();

            TransferValues(photoUploadToAdd, photoUpload);
            SetValues(photoUploadToAdd);

            _repository.Add(photoUploadToAdd);
            _unitOfWork.Save();
        }

        private void Update(PhotoUploadModel photoUpload)
        {
            PhotoUpload photoUploadToUpdate = _repository.Single(p => p.CloudId == photoUpload.CloudId);

            TransferValues(photoUploadToUpdate, photoUpload);
            SetValues(photoUploadToUpdate);

            _repository.Update(photoUploadToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(PhotoUpload toPhotoUpload, PhotoUploadModel fromPhotoUpload)
        {
            toPhotoUpload.FileName = fromPhotoUpload.FileName;
            toPhotoUpload.CloudId = fromPhotoUpload.CloudId;
            toPhotoUpload.Version = fromPhotoUpload.Version;
            toPhotoUpload.Url = fromPhotoUpload.Url;
            toPhotoUpload.Height = fromPhotoUpload.Height;
            toPhotoUpload.Width = fromPhotoUpload.Width;
        }

        private void SetValues(PhotoUpload photoUpload)
        {
            IList<string> fileNameParts = GetNameParts(photoUpload.FileName);

            photoUpload.Instance = GetIntance(fileNameParts);
            photoUpload.Kind = GetKind(photoUpload.FileName);
            photoUpload.Variants = GetVariants(photoUpload, fileNameParts);
        }

        private IList<string> GetNameParts(string fileName)
        {
            fileName = fileName.Remove(fileName.IndexOf('.'));
            string[] parts = fileName.Split('_');

            return parts.ToList();
        }

        private IList<PhotoUploadVariant> GetVariants(PhotoUpload photoUpload, IList<string> nameParts)
        {
            if (nameParts.Count < 5)
            {
                return null;
            }

            Brand brand = GetBrand(nameParts);

            if (brand == null)
            {
                return null;
            }
            else if (nameParts[2].Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                return GetVariantsForBrand(photoUpload, brand);
            }
            else
            {
                Product product = GetProduct(brand, nameParts);

                if (product == null) 
                {
                    return null;
                }
                else if (nameParts[3].Equals("all", StringComparison.OrdinalIgnoreCase) 
                    && nameParts[4].Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    return GetVariantsForProduct(photoUpload, product);
                }
                else if (!nameParts[3].Equals("all", StringComparison.OrdinalIgnoreCase)
                    && nameParts[4].Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    Size size = GetSize(product, nameParts);

                    if (size != null)
                    {
                        return GetVariantsForProductAndSize(photoUpload, product, size);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (nameParts[3].Equals("all", StringComparison.OrdinalIgnoreCase)
                    && !nameParts[4].Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    Color color = GetColor(product, nameParts);

                    if (color != null)
                    {
                        return GetVariantsForProductAndColor(photoUpload, product, color);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (!nameParts[3].Equals("all", StringComparison.OrdinalIgnoreCase)
                    && !nameParts[4].Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    Size size = GetSize(product, nameParts);
                    Color color = GetColor(product, nameParts);

                    if (size != null && color != null)
                    {
                        return GetVariantsForProductAndSizeAndColor(photoUpload, product, size, color);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private IList<PhotoUploadVariant> GetVariantsForProductAndSizeAndColor(PhotoUpload photoUpload, Product product, Size size, Color color)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.ProductId == product.Id && v.SizeId == size.Id && v.ColorId == color.Id).ToList();

            return CreatePhotoUploadVariants(photoUpload, variants);
        }

        private IList<PhotoUploadVariant> GetVariantsForProductAndColor(PhotoUpload photoUpload, Product product, Color color)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.ProductId == product.Id && v.ColorId == color.Id).ToList();

            return CreatePhotoUploadVariants(photoUpload, variants);
        }

        private IList<PhotoUploadVariant> GetVariantsForProductAndSize(PhotoUpload photoUpload, Product product, Size size)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.ProductId == product.Id && v.SizeId == size.Id).ToList();

            return CreatePhotoUploadVariants(photoUpload, variants);
        }

        private IList<PhotoUploadVariant> GetVariantsForProduct(PhotoUpload photoUpload, Product product)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.ProductId == product.Id).ToList();

            return CreatePhotoUploadVariants(photoUpload, variants);
        }

        private IList<PhotoUploadVariant> GetVariantsForBrand(PhotoUpload photoUpload, Brand brand)
        {
            IList<Variant> variants = _variantRepository.Where(v => v.Product.BrandId == brand.Id).ToList();

            return CreatePhotoUploadVariants(photoUpload, variants);
        }

        private IList<PhotoUploadVariant> CreatePhotoUploadVariants(PhotoUpload photoUpload, IList<Variant> variants)
        {
            IList<PhotoUploadVariant> photoUploadVariants = new List<PhotoUploadVariant>();

            foreach (var variant in variants)
            {
                PhotoUploadVariant photoUploadVariant = new PhotoUploadVariant()
                {
                    PhotoUpload = photoUpload,
                    Variant = variant
                };

                photoUploadVariants.Add(photoUploadVariant);
            }

            return photoUploadVariants;
        }

        private Color GetColor(Product product, IList<string> nameParts)
        {
            if (nameParts.Count < 5)
            {
                return null;
            }

            int colorPartsCount = nameParts.Count - 4;
            string colorCode = nameParts[4];

            for (int i = 1; i < colorPartsCount; i++)
            {
                colorCode += $"_{nameParts[4 + i]}";
            }

            return _colorRepository.SingleOrDefault(c => c.GroupId == product.ColorGroupId && c.Code == colorCode);
        }

        private Size GetSize(Product product, IList<string> nameParts)
        {
            if (nameParts.Count < 4)
            {
                return null;
            }

            return _sizeRepository.SingleOrDefault(s => s.GroupId == product.SizeGroupId && s.Code == nameParts[3]);
        }

        private Product GetProduct(Brand brand, IList<string> nameParts)
        {
            if (nameParts.Count < 3)
            {
                return null;
            }

            return _productRepository.SingleOrDefault(p => p.BrandId == brand.Id && p.Code == nameParts[2]);
        }

        private Brand GetBrand(IList<string> nameParts)
        {
            if (nameParts.Count < 2)
            {
                return null;
            }

            return _brandRepository.SingleOrDefault(b => b.Code == nameParts[1]);
        }

        private PhotoKind GetKind(string fileName)
        {
            if (fileName.Count(c => c == '.') < 2)
            {
                return null;
            }

            int lastIndexOfDot = fileName.LastIndexOf('.');
            fileName = fileName.Substring(0, lastIndexOfDot);

            lastIndexOfDot = fileName.LastIndexOf('.');
            string kindString = fileName.Substring(lastIndexOfDot + 1);

            foreach (var kindBeginning in _photoKindBeginningRepository.GetAll())
            {
                if (kindString.StartsWith(kindBeginning.Name, StringComparison.OrdinalIgnoreCase) 
                    && kindString.Length == kindBeginning.Name.Length + 2)
                {
                    string numberString = kindString.Replace(kindBeginning.Name, string.Empty, StringComparison.OrdinalIgnoreCase);

                    if (numberString.Contains("0"))
                    {
                        numberString = numberString.Replace("0", string.Empty);
                    }

                    int number;
                    bool isNumber = int.TryParse(numberString, out number);

                    if (isNumber)
                    {
                        if (_photoKindRepository.Any(k => k.Name == kindString))
                        {
                            return _photoKindRepository.Single(k => k.Name == kindString);
                        }
                        else
                        {
                            PhotoKind photoKindToAdd = new PhotoKind { Name = kindString.ToUpper() };
                            _photoKindRepository.Add(photoKindToAdd);
                            _unitOfWork.Save();

                            return photoKindToAdd;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        private Instance GetIntance(IList<string> nameParts)
        {
            Instance instance = _instanceRepository.SingleOrDefault(i => i.Name == nameParts[0]);

            return instance;
        }

        private IQueryable<PhotoUpload> ApplyFilter(IQueryable<PhotoUpload> photos, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return photos;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "product", "product_id", "variant", "size", "color", "instance", "type", "kind", "filename", "height", "width" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    photos = searchField.Name switch
                    {
                        "product" => photos.Where(p => p.Variants.Any(v => v.Variant.Product.Name.Contains(searchField.Value))),
                        "product_id" => photos.Where(p => p.Variants.Any(v => v.Variant.Product.Id == searchField.Value.ParseToIntOrDefault())),
                        "variant" => photos.Where(p => p.Variants.Any(v => v.Variant.Sku.Contains(searchField.Value))),
                        "size" => photos.Where(p => p.Variants.Any(v => v.Variant.Size.Name.Contains(searchField.Value))),
                        "color" => photos.Where(p => p.Variants.Any(v => v.Variant.Color.Name.Contains(searchField.Value))),
                        "instance" => photos.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        "type" => photos.Where(p => p.Type != null && p.Type.Name.Contains(searchField.Value)),
                        "kind" => photos.Where(p => p.Kind != null && p.Kind.Name.Contains(searchField.Value)),
                        "filename" => photos.Where(p => p.FileName.Contains(searchField.Value)),
                        "height" => photos.Where(p => p.Height == searchField.Value.ParseToIntOrDefault()),
                        "width" => photos.Where(p => p.Width == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    photos = photos.Where(p => p.Variants.Any(v => v.Variant.Product.Name.Contains(searchString))
                        || p.Variants.Any(v => v.Variant.Product.Id == searchString.ParseToIntOrDefault())
                        || p.Variants.Any(v => v.Variant.Sku.Contains(searchString))
                        || p.Variants.Any(v => v.Variant.Size.Name.Contains(searchString))
                        || p.Variants.Any(v => v.Variant.Color.Name.Contains(searchString))
                        || p.Instance.Name.Contains(searchString)
                        || p.Type != null && p.Type.Name.Contains(searchString)
                        || p.Kind != null && p.Kind.Name.Contains(searchString)
                        || p.FileName.Contains(searchString)
                        || p.Height == searchString.ParseToIntOrDefault()
                        || p.Width == searchString.ParseToIntOrDefault()
                        || ("not set".Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                            && p.Type == null)
                        || ("unrecognized".Contains(searchString, StringComparison.OrdinalIgnoreCase)
                            && (!p.Variants.Any() || p.Kind == null || p.Instance == null)));
                }
            }

            return photos;
        }

        private IQueryable<PhotoUpload> ApplySorting(IQueryable<PhotoUpload> photos, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                photos = request.SortBy switch
                {
                    "instance" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Instance.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Instance.Name).ThenBy(p => p.Type.Id),
                    "type" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Type.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Type.Name).ThenBy(p => p.Type.Id),
                    "kind" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Kind.Name).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Kind.Name).ThenBy(p => p.Type.Id),
                    "filename" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.FileName).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.FileName).ThenBy(p => p.Type.Id),
                    "height" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Height).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Height).ThenBy(p => p.Type.Id),
                    "width" => request.SortDirection == SortDirection.Ascending ? photos.OrderBy(p => p.Width).ThenBy(p => p.Type.Id) : photos.OrderByDescending(p => p.Width).ThenBy(p => p.Type.Id),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                photos = photos.OrderBy(p => p.FileName);
            }

            return photos;
        }
    }
}
