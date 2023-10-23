using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings
{
	public class AdditionalSkuService : IAdditionalSkuService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<AdditionalSku> _repository;

		public AdditionalSkuService(
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<AdditionalSku>();
		}

		public AdditionalSkuModel Get(long id) => _mapper.Map<AdditionalSkuModel>(_repository.Get(id));

		public int CountForListRequest(ListRequest request)
		{
			IQueryable<AdditionalSku> skus = _repository.GetQuery();

			skus = ApplyFilter(skus, request);

			return skus.Count();
		}

		public IList<AdditionalSkuModel> GetForListRequest(ListRequest request)
		{
			IQueryable<AdditionalSku> skus = _repository.GetQuery();

			skus = ApplyFilter(skus, request);
			skus = ApplySorting(skus, request);
			skus = ApplyPaging(skus, request);

			return _mapper.Map<IList<AdditionalSkuModel>>(skus);
		}

		public void Add(AdditionalSkuModel sku)
		{
			AdditionalSku skuToAdd = new();

			TransferValues(skuToAdd, sku);

			_repository.Add(skuToAdd);
			_unitOfWork.Save();
		}

		public void Update(AdditionalSkuModel sku)
		{
			AdditionalSku skuToUpdate = _repository.Get(sku.Id);

			TransferValues(skuToUpdate, sku);

			_repository.Update(skuToUpdate);
			_unitOfWork.Save();
		}

		private void TransferValues(AdditionalSku toCenter, AdditionalSkuModel fromCenter)
		{
			toCenter.VariantId = fromCenter.Variant.Id;
			toCenter.Sku = fromCenter.Sku;
		}

		private IQueryable<AdditionalSku> ApplyFilter(IQueryable<AdditionalSku> skus, ListRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return skus;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "variant", "sku" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					skus = searchField.Name switch
					{
						"id" => skus.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
						"variant" => skus.Where(s => s.Variant.Sku.Contains(searchField.Value)),
						"sku" => skus.Where(s => s.Sku.Contains(searchField.Value)),
						_ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					skus = skus.Where(s => s.Id == searchString.ParseToIntOrDefault()
						|| s.Variant.Sku.Contains(searchString)
						|| s.Sku.Contains(searchString));
				}
			}

			return skus;
		}

		private IQueryable<AdditionalSku> ApplySorting(IQueryable<AdditionalSku> skus, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				skus = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? skus.OrderBy(s => s.Id) : skus.OrderByDescending(s => s.Id),
					"variant" => request.SortDirection == SortDirection.Ascending ? skus.OrderBy(s => s.Variant.Sku) : skus.OrderByDescending(s => s.Variant.Sku),
					"sku" => request.SortDirection == SortDirection.Ascending ? skus.OrderBy(s => s.Sku) : skus.OrderByDescending(s => s.Sku),
					_ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				skus = skus.OrderBy(s => s.Id);
			}

			return skus;
		}

		private IQueryable<AdditionalSku> ApplyPaging(IQueryable<AdditionalSku> skus, ListRequest request)
		{
			return skus.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
