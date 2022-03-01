using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class KeywordResearchService : IKeywordResearchService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<KeywordResearch> _repository;

        public KeywordResearchService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<KeywordResearch>();
        }

        public KeywordResearchModel Get(long id) => _mapper.Map<KeywordResearchModel>(_repository.Get(id));

        public IList<KeywordResearchModel> GetAll() => _mapper.Map<IList<KeywordResearchModel>>(_repository.GetQuery().OrderBy(r => r.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<KeywordResearch> researches = _repository.GetQuery();

            ApplyFilter(researches, request);

            return researches.Count();
        }

        public IList<KeywordResearchModel> GetForListRequest(ListRequest request)
        {
            IQueryable<KeywordResearch> researches = _repository.GetQuery();

            researches = ApplyFilter(researches, request);
            researches = ApplySorting(researches, request);
            researches = ApplyPaging(researches, request);

            return _mapper.Map<IList<KeywordResearchModel>>(researches);
        }

        public void Add(KeywordResearchModel research)
        {
            KeywordResearch researchToAdd = new();

            TransferValues(researchToAdd, research);

            _repository.Add(researchToAdd);
            _unitOfWork.Save();
        }

        public void Update(KeywordResearchModel research)
        {
            KeywordResearch researchToUpdate = _repository.Get(research.Id);

            TransferValues(researchToUpdate, research);

            _repository.Update(researchToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(KeywordResearch toResearch, KeywordResearchModel fromResearch)
        {
            toResearch.Name = fromResearch.Name;
        }

        private IQueryable<KeywordResearch> ApplyFilter(IQueryable<KeywordResearch> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => products.Where(p => p.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<KeywordResearch> ApplySorting(IQueryable<KeywordResearch> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<KeywordResearch> ApplyPaging(IQueryable<KeywordResearch> researches, ListRequest request)
        {
            return researches.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
