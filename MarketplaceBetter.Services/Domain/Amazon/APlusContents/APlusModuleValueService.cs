using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.VisualBasic;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;
using static System.Net.Mime.MediaTypeNames;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
	public class APlusModuleValueService : IAPlusModuleValueService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<APlusModuleValue> _repository;
        private readonly IRepository<APlusImage> _imageRepository;
        private readonly IUserService _userService;
		private readonly IAPlusImageCloudService _imageCloudService;

		public APlusModuleValueService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
			IUserService userService,
            IAPlusImageCloudService imageCloudService)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<APlusModuleValue>();
			_imageRepository = unitOfWork.GetRepository<APlusImage>();
			_userService = userService;
			_imageCloudService = imageCloudService;

        }

		public APlusModuleValueModel Get(long id) => _mapper.Map<APlusModuleValueModel>(_repository.Get(id));

		public IList<APlusModuleValueModel> GetAll()
		{
			BrandModel currentBrand = _userService.GetCurrentBrand();
			if (currentBrand != null && _userService.IsSpecificBrand())
			{
				return _mapper.Map<IList<APlusModuleValueModel>>(_repository.Where(m => m.Content.Product.BrandId == currentBrand.Id).OrderBy(m => m.Content.Name).ThenBy(m => m.Order));
			}
			else
			{
				return _mapper.Map<IList<APlusModuleValueModel>>(_repository.GetQuery().OrderBy(m => m.Content.Name).ThenBy(m => m.Order));
			}
		}

		public int CountForListRequest(ListRequest request)
		{
			IQueryable<APlusModuleValue> modules = _repository.GetQuery();

			modules = ApplyFilter(modules, request);

			return modules.Count();
		}

		public IList<APlusModuleValueModel> GetForListRequest(ListRequest request)
		{
			IQueryable<APlusModuleValue> modules = _repository.GetQuery();

			modules = ApplyFilter(modules, request);
			modules = ApplySorting(modules, request);
			modules = ApplyPaging(modules, request);

			return _mapper.Map<IList<APlusModuleValueModel>>(modules);
		}

		public IList<APlusModuleValueModel> GetAllChartsForProduct(long productId, int order)
		{
            IQueryable<APlusModuleValue> modules = _repository.Where(m => m.Module.SystemName == APlusModuleEnum.StandardComparisonChart && m.Order == order && m.Content.ProductId == productId).OrderBy(m => m.Content.Name);

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (currentInstance != null && _userService.IsSpecificInstance())
            {
                modules = modules.Where(m => m.Content.InstanceId == currentInstance.Id);
            }

            return _mapper.Map<IList<APlusModuleValueModel>>(modules);
        }

        public int GetNextOrder(long contentId)
		{
			int nextOrder = 1;

			if (_repository.Any(m => m.ContentId == contentId))
			{
				nextOrder = _repository.Where(m => m.ContentId == contentId).Max(m => m.Order) + 1;
			}

			return nextOrder;
		}

        public void Add(APlusModuleValueModel module)
		{
			APlusModuleValue moduleToAdd = new();

			TransferValues(moduleToAdd, module);

			_repository.Add(moduleToAdd);
			_unitOfWork.Save();
		}

		public void Update(APlusModuleValueModel module, IList<string> imagesToDelete)
		{
			APlusModuleValue moduleToUpdate = _repository.Get(module.Id);

			foreach (var element in module.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && e.Image != null))
			{
				imagesToDelete.Remove(element.Element.Name);
			}

			DeleteImages(moduleToUpdate, imagesToDelete);

			TransferValues(moduleToUpdate, module);

			_repository.Update(moduleToUpdate);
			_unitOfWork.Save();
		}

		public void Delete(long id)
		{
			APlusModuleValue module = _repository.Get(id);

			DeleteImages(module);

			_repository.Delete(module);
			_unitOfWork.Save();
		}

		private void DeleteImages(APlusModuleValue module, IList<string> imagesToDelete)
		{
            IList<APlusImage> images = module.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && 
				e.Image != null && imagesToDelete.Contains(e.Element.Name)).Select(e => e.Image).ToList();

            DeleteImages(images);
        }

        private void DeleteImages(APlusModuleValue module)
        {
            IList<APlusImage> images = module.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && e.Image != null)
                .Select(e => e.Image).ToList();

            DeleteImages(images);
        }

        private void DeleteImages(IList<APlusImage> images)
		{
            IList<string> cloudIds = images.Select(i => i.CloudId).ToList();

            _imageCloudService.Delete(cloudIds);

            foreach (var image in images)
            {
                _imageRepository.Delete(image);
            }

            _unitOfWork.Save();
        }

        private void TransferValues(APlusModuleValue toModule, APlusModuleValueModel fromModule)
		{
			toModule.ContentId = fromModule.Content.Id;
            toModule.Order = fromModule.Order;
            toModule.ModuleId = fromModule.Module.Id;

			foreach (var fromElement in fromModule.Elements)
			{
				if (toModule.Id == 0)
				{
                    APlusElementValue toElement = new APlusElementValue();

					TransferValues(fromModule, toElement, fromElement);

					toModule.Elements.Add(toElement);
				}
				else
				{
                    APlusElementValue toElement = toModule.Elements.Single(e => e.Id == fromElement.Id);

					TransferValues(fromModule, toElement, fromElement);
				}
			}
		}

		private void TransferValues(APlusModuleValueModel fromModule, APlusElementValue toElement, APlusElementValueModel fromElement)
		{
			toElement.ElementId = fromElement.Element.Id;
            toElement.SingleLineText = fromElement.SingleLineText;
			if (fromElement.Element.Type.SystemName == APlusElementTypeEnum.BodyText && (fromElement.BodyText == null || (!fromElement.BodyText.Equals(fromElement.Element.Name, StringComparison.OrdinalIgnoreCase)
                && !fromElement.BodyText.Equals($"<p>{fromElement.Element.Name}</p>", StringComparison.OrdinalIgnoreCase))))
			{
                toElement.BodyText = fromElement.BodyText;
            }
			toElement.TrueOrFalse = fromElement.TrueOrFalse;

            APlusImageModel fromImage = fromElement.Image;
            if (fromImage != null)
			{
				if (fromImage.Id == 0 && toElement.Image == null)
				{
					APlusImage toImage = new APlusImage();

					TransferValues(toImage, fromImage);

					toElement.Image = toImage;
				}
				else if (fromImage.Id != 0 && toElement.Image == null)
				{
                    string extension = fromImage.FileName.Substring(fromImage.FileName.LastIndexOf('.') + 1);
                    string shortName = $"{fromModule.Order}_{fromModule.Module.Name.Replace("&", string.Empty).WithoutSpaces()}_{fromElement.Element.Name.WithoutSpaces()}.{extension}";
                    string fullName = $"{fromModule.Content.Name.WithoutSpaces()}/{shortName}";

                    APlusImage toImage = _imageCloudService.CopyImage(fromImage.CloudId, fromImage.Version, fullName);
					toImage.FileName = shortName;

                    toElement.Image = toImage;
                }
				else
				{
					TransferValues(toElement.Image, fromImage);
				}
			}
        }

		private void TransferValues(APlusImage toImage, APlusImageModel fromImage)
		{
			toImage.CloudId = fromImage.CloudId;
			toImage.Version = fromImage.Version;
			toImage.Url = fromImage.Url;
			toImage.FileName = fromImage.FileName;
		}

		private IQueryable<APlusModuleValue> ApplyFilter(IQueryable<APlusModuleValue> modules, ListRequest request)
		{
			BrandModel currentBrand = _userService.GetCurrentBrand();
			if (currentBrand != null && _userService.IsSpecificBrand())
			{
				modules = modules.Where(m => m.Content.Product.BrandId == currentBrand.Id);
			}

			InstanceModel currentInstance = _userService.GetCurrentInstance();
			if (currentInstance != null && _userService.IsSpecificInstance())
			{
				modules = modules.Where(m => m.Content.InstanceId == currentInstance.Id);
			}

			bool showDrafts = _userService.ShowDrafts();
			if (!showDrafts)
			{
				modules = modules.Where(m => m.Content.Status.SystemName != EntityStatusEnum.Draft);
			}

			bool showWithdrawn = _userService.ShowWithdrawn();
			if (!showWithdrawn)
			{
				modules = modules.Where(m => m.Content.Status.SystemName != EntityStatusEnum.Withdrawn);
			}

			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return modules;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "content", "product", "variant", "name", "order", "instance" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					modules = searchField.Name switch
					{
						"id" => modules.Where(m => m.Id == searchField.Value.ParseToIntOrDefault()),
						"content" => modules.Where(m => m.Content.Name.Contains(searchField.Value)),
                        "product" => modules.Where(m => m.Content.Product.Code.Contains(searchField.Value)),
                        "variant" => modules.Where(m => m.Content.Variants.Any(v => v.Variant.Sku.Contains(searchField.Value) || v.Variant.Asin.Contains(searchField.Value))
                            || (m.Content.AllVariants && m.Content.Product.Variants.Any(v => v.Sku.Contains(searchField.Value) || v.Asin.Contains(searchField.Value)))),
                        "name" => modules.Where(m => m.Module.Name.Contains(searchField.Value)),
						"order" => modules.Where(m => m.Order == searchField.Value.ParseToIntOrDefault()),
                        "instance" => modules.Where(m => m.Content.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					modules = modules.Where(m => m.Id == searchString.ParseToIntOrDefault()
						|| m.Content.Name.Contains(searchString)
                        || m.Content.Product.Code.Contains(searchString)
                        || m.Content.Variants.Any(v => v.Variant.Sku.Contains(searchString) || v.Variant.Asin.Contains(searchString))
                        || (m.Content.AllVariants && m.Content.Product.Variants.Any(v => v.Sku.Contains(searchString) || v.Asin.Contains(searchString)))
                        || m.Module.Name.Contains(searchString)
						|| m.Order == searchString.ParseToIntOrDefault()
                        || m.Content.Instance.Name.Contains(searchString));
				}
			}

			return modules;
		}

		private IQueryable<APlusModuleValue> ApplySorting(IQueryable<APlusModuleValue> modules, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				modules = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Id) : modules.OrderByDescending(m => m.Id),
                    "content" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Content.Name) : modules.OrderByDescending(m => m.Content.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Content.Product.Code) : modules.OrderByDescending(m => m.Content.Product.Code),
                    "name" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Module.Name) : modules.OrderByDescending(m => m.Module.Name),
					"order" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Order) : modules.OrderByDescending(m => m.Order),
                    "instance" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Content.Instance.Name) : modules.OrderByDescending(m => m.Content.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				modules = modules.OrderBy(m => m.Content.Name).ThenBy(m => m.Order);
			}

			return modules;
		}

		private IQueryable<APlusModuleValue> ApplyPaging(IQueryable<APlusModuleValue> modules, ListRequest request)
		{
			return modules.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
