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

        public VatRuleModel GetFor(long countryFromId, long countryToId) => _mapper.Map<VatRuleModel>(_repository.SingleOrDefault(r => r.CountryFromId == countryFromId
            && r.CountryToId == countryToId));


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

        private void TransferValues(VatRule toRule, VatRuleModel fromRule)
        {
            toRule.CountryFromId = fromRule.CountryFrom.Id;
            toRule.CountryToId = fromRule.CountryTo.Id;
            toRule.VatValue = fromRule.VatValue;
            toRule.VatNumber = fromRule.VatNumber;
            toRule.InvoiceNextNumber = fromRule.InvoiceNextNumber;
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
                string[] searchFieldNames = new[] { "id", "country_from", "country_to", "vat_value", "vat_number", "invoice_next_number" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    rules = searchField.Name switch
                    {
                        "id" => rules.Where(r => r.Id == searchField.Value.ParseToIntOrDefault()),
                        "country_from" => rules.Where(r => r.CountryFrom.Name.Contains(searchField.Value) || r.CountryFrom.Code.Contains(searchField.Value)),
                        "country_to" => rules.Where(r => r.CountryTo.Name.Contains(searchField.Value) || r.CountryTo.Code.Contains(searchField.Value)),
                        "vat_value" => rules.Where(r => r.VatValue == searchField.Value.ParseToDoubleOrDefault()),
                        "vat_number" => rules.Where(r => r.VatNumber.Contains(searchField.Value)),
                        "invoice_next_number" => rules.Where(r => r.InvoiceNextNumber == searchField.Value.ParseToIntOrDefault()),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    rules = rules.Where(r => r.Id == searchString.ParseToIntOrDefault()
                        || r.CountryFrom.Name.Contains(searchString)
                        || r.CountryFrom.Code.Contains(searchString)
                        || r.CountryTo.Name.Contains(searchString)
                        || r.CountryTo.Code.Contains(searchString)
                        || r.VatValue == searchString.ParseToDoubleOrDefault()
                        || r.VatNumber.Contains(searchString)
                        || r.InvoiceNextNumber == searchString.ParseToIntOrDefault());
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
                    "invoice_next_number" => request.SortDirection == SortDirection.Ascending ? rules.OrderBy(r => r.InvoiceNextNumber) : rules.OrderByDescending(r => r.InvoiceNextNumber),
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
