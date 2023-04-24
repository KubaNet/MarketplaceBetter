using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
	public class APlusSectionValueService : IAPlusSectionValueService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<APlusSectionValue> _repository;
		private readonly IUserService _userService;

		public APlusSectionValueService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
			IUserService userService)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<APlusSectionValue>();
			_userService = userService;
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
			IQueryable<APlusSectionValue> contents = _repository.GetQuery();

			contents = ApplyFilter(contents, request);

			return contents.Count();
		}

		public IList<APlusSectionValueModel> GetForListRequest(ListRequest request)
		{
			IQueryable<APlusSectionValue> contents = _repository.GetQuery();

			contents = ApplyFilter(contents, request);
			contents = ApplySorting(contents, request);
			contents = ApplyPaging(contents, request);

			return _mapper.Map<IList<APlusSectionValueModel>>(contents);
		}

		public void Add(APlusSectionValueModel content)
		{
			APlusSectionValue contentToAdd = new();

			TransferValues(contentToAdd, content);

			_repository.Add(contentToAdd);
			_unitOfWork.Save();
		}

		public void Update(APlusSectionValueModel content)
		{
			APlusSectionValue contentToUpdate = _repository.Get(content.Id);

			TransferValues(contentToUpdate, content);

			_repository.Update(contentToUpdate);
			_unitOfWork.Save();
		}

		private void TransferValues(APlusSectionValue toAPlusContent, APlusSectionValueModel fromAPlusContent)
		{
			toAPlusContent.ContentId = fromAPlusContent.Content.Id;
			toAPlusContent.SectionId = fromAPlusContent.Section.Id;
			toAPlusContent.Order = fromAPlusContent.Order;
		}

		private IQueryable<APlusSectionValue> ApplyFilter(IQueryable<APlusSectionValue> contents, ListRequest request)
		{
			BrandModel currentBrand = _userService.GetCurrentBrand();
			if (currentBrand != null && _userService.IsSpecificBrand())
			{
				contents = contents.Where(s => s.Content.Product.BrandId == currentBrand.Id);
			}

			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return contents;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "content", "type", "order" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					contents = searchField.Name switch
					{
						"id" => contents.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
						"content" => contents.Where(s => s.Content.Name.Contains(searchField.Value)),
						"type" => contents.Where(s => s.Section.Type.Name.Contains(searchField.Value)),
						"order" => contents.Where(s => s.Order == searchField.Value.ParseToIntOrDefault()),
						_ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					contents = contents.Where(s => s.Id == searchString.ParseToIntOrDefault()
						|| s.Content.Name.Contains(searchString)
						|| s.Section.Type.Name.Contains(searchString)
						|| s.Order == searchString.ParseToIntOrDefault());
				}
			}

			return contents;
		}

		private IQueryable<APlusSectionValue> ApplySorting(IQueryable<APlusSectionValue> contents, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				contents = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(s => s.Id) : contents.OrderByDescending(s => s.Id),
					"content" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(s => s.Content.Name) : contents.OrderByDescending(s => s.Content.Name),
					"type" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(s => s.Section.Type.Name) : contents.OrderByDescending(s => s.Section.Type.Name),
					"order" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(s => s.Order) : contents.OrderByDescending(s => s.Order),
					_ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				contents = contents.OrderBy(s => s.Content.Name).ThenBy(s => s.Order);
			}

			return contents;
		}

		private IQueryable<APlusSectionValue> ApplyPaging(IQueryable<APlusSectionValue> contents, ListRequest request)
		{
			return contents.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
