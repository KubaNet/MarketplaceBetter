using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class VariantPhotoNamingHelper : IVariantPhotoNamingHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<Color> _colorRepository;
        private readonly IRepository<Size> _sizeRepository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IRepository<PhotoKind> _photoKindRepository;
        private readonly IRepository<PhotoKindBeginning> _photoKindBeginningRepository;

        public VariantPhotoNamingHelper(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = unitOfWork.GetRepository<Brand>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
             _colorRepository = unitOfWork.GetRepository<Color>();
            _sizeRepository = unitOfWork.GetRepository<Size>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _photoKindRepository = unitOfWork.GetRepository<PhotoKind>();
            _photoKindBeginningRepository = unitOfWork.GetRepository<PhotoKindBeginning>();
        }

        public IList<Variant> GetVariantsFrom(string fileName)
        {
            IList<string> nameParts = GetNameParts(fileName);

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
                return _variantRepository.Where(v => v.Product.BrandId == brand.Id).ToList();
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
                    return _variantRepository.Where(v => v.ProductId == product.Id).ToList();
                }
                else if (!nameParts[3].Equals("all", StringComparison.OrdinalIgnoreCase)
                    && nameParts[4].Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    Size size = GetSize(product, nameParts);

                    if (size != null)
                    {
                        return _variantRepository.Where(v => v.ProductId == product.Id && v.SizeId == size.Id).ToList();
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
                        return _variantRepository.Where(v => v.ProductId == product.Id && v.ColorId == color.Id).ToList();
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
                        return _variantRepository.Where(v => v.ProductId == product.Id && v.SizeId == size.Id && v.ColorId == color.Id).ToList();
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

        public Instance GetInstanceFrom(string fileName)
        {
            IList<string> nameParts = GetNameParts(fileName);
            Instance instance = _instanceRepository.SingleOrDefault(i => i.Name == nameParts[0]);

            return instance;
        }

        public PhotoKind GetPhotoKindFrom(string fileName)
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

        private IList<string> GetNameParts(string fileName)
        {
            fileName = fileName.Remove(fileName.IndexOf('.'));
            string[] parts = fileName.Split('_');

            return parts.ToList();
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

            Product product = _productRepository.SingleOrDefault(p => p.BrandId == brand.Id && p.Code == nameParts[2]);

            if (product != null)
            {
                return product;
            }

            string productCode = string.Format("{0}_{1}", nameParts[2], nameParts[3]);

            return _productRepository.SingleOrDefault(p => p.BrandId == brand.Id && p.Code == productCode);
        }

        private Brand GetBrand(IList<string> nameParts)
        {
            if (nameParts.Count < 2)
            {
                return null;
            }

            return _brandRepository.SingleOrDefault(b => b.Code == nameParts[1]);
        }
    }
}
