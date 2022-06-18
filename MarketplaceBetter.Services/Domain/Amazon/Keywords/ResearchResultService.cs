using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords
{
    public class ResearchResultService : IResearchResultService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ResearchResult> _repository;

        public ResearchResultService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ResearchResult>();
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ResearchResult> results = _repository.GetQuery();

            ApplyFilter(results, request);

            return results.Count();
        }

        public IList<ResearchResultModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ResearchResult> results = _repository.GetQuery();

            results = ApplyFilter(results, request);
            results = ApplySorting(results, request);
            results = ApplyPaging(results, request);

            return _mapper.Map<IList<ResearchResultModel>>(results);
        }

        private IQueryable<ResearchResult> ApplyFilter(IQueryable<ResearchResult> results, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return results;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "phrase", "research" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    results = searchField.Name switch
                    {
                        "id" => results.Where(r => r.Id == searchField.Value.ParseToIntOrDefault()),
                        "phrase" => results.Where(r => r.Phrase.Contains(searchField.Value)),
                        "research" => results.Where(r => r.Research.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    results = results.Where(r => r.Id == searchString.ParseToIntOrDefault()
                        || r.Phrase.Contains(searchString)
                        || r.Research.Name.Contains(searchString));
                }
            }

            return results;
        }

        private IQueryable<ResearchResult> ApplySorting(IQueryable<ResearchResult> results, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                results = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? results.OrderBy(r => r.Id) : results.OrderByDescending(r => r.Id),
                    "phrase" => request.SortDirection == SortDirection.Ascending ? results.OrderBy(r => r.Phrase) : results.OrderByDescending(r => r.Phrase),
                    "score" => request.SortDirection == SortDirection.Ascending ? results.OrderBy(r => r.Score) : results.OrderByDescending(r => r.Score),
                    "research" => request.SortDirection == SortDirection.Ascending ? results.OrderBy(r => r.Research.Name) : results.OrderByDescending(r => r.Research.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return results;
        }

        private IQueryable<ResearchResult> ApplyPaging(IQueryable<ResearchResult> results, ListRequest request)
        {
            return results.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
