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

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
	public class APlusSectionValueService : IAPlusSectionValueService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<APlusSectionValue> _repository;
        private readonly IRepository<APlusImage> _imageRepository;
        private readonly IUserService _userService;
		private readonly IAPlusImageCloudService _imageCloudService;

		public APlusSectionValueService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
			IUserService userService,
            IAPlusImageCloudService imageCloudService)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<APlusSectionValue>();
			_imageRepository = unitOfWork.GetRepository<APlusImage>();
			_userService = userService;
			_imageCloudService = imageCloudService;

        }

		public APlusSectionValueModel Get(long id) => _mapper.Map<APlusSectionValueModel>(_repository.Get(id));

		public IList<APlusSectionValueModel> GetAll()
		{
			BrandModel currentBrand = _userService.GetCurrentBrand();
			if (currentBrand != null && _userService.IsSpecificBrand())
			{
				return _mapper.Map<IList<APlusSectionValueModel>>(_repository.Where(s => s.Content.Product.BrandId == currentBrand.Id).OrderBy(s => s.Content.Name).ThenBy(s => s.Order));
			}
			else
			{
				return _mapper.Map<IList<APlusSectionValueModel>>(_repository.GetQuery().OrderBy(s => s.Content.Name).ThenBy(s => s.Order));
			}
		}

		public int CountForListRequest(ListRequest request)
		{
			IQueryable<APlusSectionValue> sections = _repository.GetQuery();

			sections = ApplyFilter(sections, request);

			return sections.Count();
		}

		public IList<APlusSectionValueModel> GetForListRequest(ListRequest request)
		{
			IQueryable<APlusSectionValue> sections = _repository.GetQuery();

			sections = ApplyFilter(sections, request);
			sections = ApplySorting(sections, request);
			sections = ApplyPaging(sections, request);

			return _mapper.Map<IList<APlusSectionValueModel>>(sections);
		}

		public int GetNextOrder(long contentId)
		{
			int nextOrder = 1;

			if (_repository.Any(s => s.ContentId == contentId))
			{
				nextOrder = _repository.Where(s => s.ContentId == contentId).Max(s => s.Order) + 1;
			}

			return nextOrder;
		}

        public void Add(APlusSectionValueModel section)
		{
			APlusSectionValue sectionToAdd = new();

			TransferValues(sectionToAdd, section);

			_repository.Add(sectionToAdd);
			_unitOfWork.Save();
		}

		public void Update(APlusSectionValueModel section, IList<string> imagesToDelete)
		{
			APlusSectionValue sectionToUpdate = _repository.Get(section.Id);

			foreach (var element in section.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && e.Image != null))
			{
				imagesToDelete.Remove(element.Element.Name);
			}

			DeleteImages(sectionToUpdate, imagesToDelete);
			TransferValues(sectionToUpdate, section);

			_repository.Update(sectionToUpdate);
			_unitOfWork.Save();
		}

		public void Delete(long id)
		{
			APlusSectionValue section = _repository.Get(id);

			DeleteImages(section);

			_repository.Delete(section);
			_unitOfWork.Save();
		}

		private void DeleteImages(APlusSectionValue section, IList<string> imagesToDelete)
		{
            IList<APlusImage> images = section.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && 
				e.Image != null && imagesToDelete.Contains(e.Element.Name)).Select(e => e.Image).ToList();

            DeleteImages(images);
        }

        private void DeleteImages(APlusSectionValue section)
        {
            IList<APlusImage> images = section.Elements.Where(e => e.Element.Type.SystemName == APlusElementTypeEnum.Image && e.Image != null)
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

        private void TransferValues(APlusSectionValue toSection, APlusSectionValueModel fromSection)
		{
			toSection.ContentId = fromSection.Content.Id;
            toSection.Order = fromSection.Order;
            toSection.SectionId = fromSection.Section.Id;

			foreach (var fromElement in fromSection.Elements)
			{
				if (toSection.Id == 0)
				{
                    APlusElementValue toElement = new APlusElementValue();

					TransferValues(toElement, fromElement);

					toSection.Elements.Add(toElement);
				}
				else
				{
                    APlusElementValue toElement = toSection.Elements.Single(e => e.Id == fromElement.Id);

					TransferValues(toElement, fromElement);
				}
			}
		}

		private void TransferValues(APlusElementValue toElement, APlusElementValueModel fromElement)
		{
			toElement.ElementId = fromElement.Element.Id;
            toElement.SingleLineText = fromElement.SingleLineText;
			if (fromElement.Element.Type.SystemName == APlusElementTypeEnum.BodyText && (fromElement.BodyText == null || (!fromElement.BodyText.Equals(fromElement.Element.Name, StringComparison.OrdinalIgnoreCase)
                && !fromElement.BodyText.Equals($"<p>{fromElement.Element.Name}</p>", StringComparison.OrdinalIgnoreCase))))
			{
                toElement.BodyText = fromElement.BodyText;
            }

			if (fromElement.Image != null)
			{
				if (fromElement.Image.Id == 0 && toElement.Image == null)
				{
					APlusImage toImage = new APlusImage();

					TransferValues(toImage, fromElement.Image);

					toElement.Image = toImage;
				}
				else
				{
					TransferValues(toElement.Image, fromElement.Image);
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

		private IQueryable<APlusSectionValue> ApplyFilter(IQueryable<APlusSectionValue> sections, ListRequest request)
		{
			BrandModel currentBrand = _userService.GetCurrentBrand();
			if (currentBrand != null && _userService.IsSpecificBrand())
			{
				sections = sections.Where(s => s.Content.Product.BrandId == currentBrand.Id);
			}

			InstanceModel currentInstance = _userService.GetCurrentInstance();
			if (currentInstance != null && _userService.IsSpecificInstance())
			{
				sections = sections.Where(s => s.Content.InstanceId == currentInstance.Id);
			}

			bool showDrafts = _userService.ShowDrafts();
			if (!showDrafts)
			{
				sections = sections.Where(s => s.Content.Status.SystemName != EntityStatusEnum.Draft);
			}

			bool showWithdrawn = _userService.ShowWithdrawn();
			if (!showWithdrawn)
			{
				sections = sections.Where(s => s.Content.Status.SystemName != EntityStatusEnum.Withdrawn);
			}

			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return sections;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "content", "product", "variant", "name", "order", "instance" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					sections = searchField.Name switch
					{
						"id" => sections.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
						"content" => sections.Where(s => s.Content.Name.Contains(searchField.Value)),
                        "product" => sections.Where(s => s.Content.Product.Code.Contains(searchField.Value)),
                        "variant" => sections.Where(s => s.Content.Variants.Any(v => v.Variant.Sku.Contains(searchField.Value) || v.Variant.Asin.Contains(searchField.Value))),
                        "name" => sections.Where(s => s.Section.Name.Contains(searchField.Value)),
						"order" => sections.Where(s => s.Order == searchField.Value.ParseToIntOrDefault()),
                        "instance" => sections.Where(s => s.Content.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					sections = sections.Where(s => s.Id == searchString.ParseToIntOrDefault()
						|| s.Content.Name.Contains(searchString)
                        || s.Content.Product.Code.Contains(searchString)
                        || s.Content.Variants.Any(v => v.Variant.Sku.Contains(searchString) || v.Variant.Asin.Contains(searchString))
                        || s.Section.Name.Contains(searchString)
						|| s.Order == searchString.ParseToIntOrDefault()
                        || s.Content.Instance.Name.Contains(searchString));
				}
			}

			return sections;
		}

		private IQueryable<APlusSectionValue> ApplySorting(IQueryable<APlusSectionValue> sections, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				sections = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Id) : sections.OrderByDescending(s => s.Id),
                    "content" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Content.Name) : sections.OrderByDescending(s => s.Content.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Content.Product.Code) : sections.OrderByDescending(s => s.Content.Product.Code),
                    "name" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Section.Name) : sections.OrderByDescending(s => s.Section.Name),
					"order" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Order) : sections.OrderByDescending(s => s.Order),
                    "instance" => request.SortDirection == SortDirection.Ascending ? sections.OrderBy(s => s.Content.Instance.Name) : sections.OrderByDescending(s => s.Content.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				sections = sections.OrderBy(s => s.Content.Name).ThenBy(s => s.Order);
			}

			return sections;
		}

		private IQueryable<APlusSectionValue> ApplyPaging(IQueryable<APlusSectionValue> sections, ListRequest request)
		{
			return sections.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
