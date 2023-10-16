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
	public class VatRuleService : IVatRuleService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<VatRule> _repository;

		public VatRuleService(
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_repository = unitOfWork.GetRepository<VatRule>();
		}

		public VatRuleModel Get(long id) => _mapper.Map<VatRuleModel>(_repository.Get(id));

		public int CountForListRequest(ListRequest request)
		{
			IQueryable<VatRule> rules = _repository.GetQuery();

			rules = ApplyFilter(rules, request);

			return rules.Count();
		}

		public IList<VatRuleModel> GetForListRequest(ListRequest request)
		{
			IQueryable<VatRule> rules = _repository.GetQuery();

			rules = ApplyFilter(rules, request);
			rules = ApplySorting(rules, request);
			rules = ApplyPaging(rules, request);

			return _mapper.Map<IList<VatRuleModel>>(rules);
		}

		public void Add(VatRuleModel rule)
		{
			VatRule ruleToAdd = new();

			TransferValues(ruleToAdd, rule);

			_repository.Add(ruleToAdd);
			_unitOfWork.Save();
		}

		public void Update(VatRuleModel rule)
		{
			VatRule ruleToUpdate = _repository.Get(rule.Id);

			TransferValues(ruleToUpdate, rule);

			_repository.Update(ruleToUpdate);
			_unitOfWork.Save();
		}

		private void TransferValues(VatRule toCenter, VatRuleModel fromCenter)
		{
			toCenter.CountryFromId = fromCenter.CountryFrom.Id;
			toCenter.CountryToId = fromCenter.CountryTo.Id;
			toCenter.VatValue = fromCenter.VatValue;
			toCenter.VatNumber = fromCenter.VatNumber;
		}

		private IQueryable<VatRule> ApplyFilter(IQueryable<VatRule> rules, ListRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.SearchString))
			{
				return rules;
			}

			IList<string> searchStrings = request.SearchString.SplitForFiltering();

			foreach (string searchString in searchStrings)
			{
				string[] searchFieldNames = new[] { "id", "country_from", "country_to", "vat_value", "vat_number" };
				SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

				if (searchField != null)
				{
					rules = searchField.Name switch
					{
						"id" => rules.Where(r => r.Id == searchField.Value.ParseToIntOrDefault()),
						"country_from" => rules.Where(r => r.CountryFrom.Name.Contains(searchField.Value)),
						"country_to" => rules.Where(r => r.CountryTo.Name.Contains(searchField.Value)),
						"vat_value" => rules.Where(r => r.VatValue == searchField.Value.ParseToIntOrDefault()),
						"vat_number" => rules.Where(r => r.VatNumber.Contains(searchField.Value)),
						_ => throw new UnrecognizedSearchFieldException(searchField.Name)
					};
				}
				else
				{
					rules = rules.Where(r => r.Id == searchString.ParseToIntOrDefault()
						|| r.CountryFrom.Name.Contains(searchString)
						|| r.CountryTo.Name.Contains(searchString)
						|| r.VatValue == searchString.ParseToIntOrDefault()
						|| r.VatNumber.Contains(searchString));
				}
			}

			return rules;
		}

		private IQueryable<VatRule> ApplySorting(IQueryable<VatRule> rules, ListRequest request)
		{
			if (!string.IsNullOrWhiteSpace(request.SortBy))
			{
				rules = request.SortBy switch
				{
					"id" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.Id) : rules.OrderByDescending(r => r.Id),
					"country_from" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.CountryFrom.Name) : rules.OrderByDescending(r => r.CountryFrom.Name),
					"country_to" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.CountryTo.Name) : rules.OrderByDescending(r => r.CountryTo.Name),
					"vat_value" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.VatValue) : rules.OrderByDescending(r => r.VatValue),
					"vat_number" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.VatNumber) : rules.OrderByDescending(r => r.VatNumber),
					_ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
				};
			}
			else
			{
				rules = rules.OrderBy(r => r.Id);
			}

			return rules;
		}

		private IQueryable<VatRule> ApplyPaging(IQueryable<VatRule> rules, ListRequest request)
		{
			return rules.Skip(request.Page * request.PageSize).Take(request.PageSize);
		}
	}
}
