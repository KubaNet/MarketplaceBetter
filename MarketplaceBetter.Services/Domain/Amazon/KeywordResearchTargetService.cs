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
    public class KeywordResearchTargetService : IKeywordResearchTargetService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<KeywordResearchTarget> _repository;

        public KeywordResearchTargetService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<KeywordResearchTarget>();
        }

        public KeywordResearchTargetModel Get(long id) => _mapper.Map<KeywordResearchTargetModel>(_repository.Get(id));

        public IList<KeywordResearchTargetModel> GetAll() => _mapper.Map<IList<KeywordResearchTargetModel>>(_repository.GetQuery().OrderBy(r => r.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<KeywordResearchTarget> targets = _repository.GetQuery();

            ApplyFilter(targets, request);

            return targets.Count();
        }

        public IList<KeywordResearchTargetModel> GetForListRequest(ListRequest request)
        {
            IQueryable<KeywordResearchTarget> targets = _repository.GetQuery();

            targets = ApplyFilter(targets, request);
            targets = ApplySorting(targets, request);
            targets = ApplyPaging(targets, request);

            return _mapper.Map<IList<KeywordResearchTargetModel>>(targets);
        }

        public void Add(KeywordResearchTargetModel target)
        {
            KeywordResearchTarget targetToAdd = new();

            TransferValues(targetToAdd, target);

            _repository.Add(targetToAdd);
            _unitOfWork.Save();
        }

        public void Update(KeywordResearchTargetModel target)
        {
            KeywordResearchTarget targetToUpdate = _repository.Get(target.Id);

            TransferValues(targetToUpdate, target);

            _repository.Update(targetToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(KeywordResearchTarget toResearch, KeywordResearchTargetModel fromResearch)
        {
            toResearch.Name = fromResearch.Name;
            toResearch.ResearchId = fromResearch.Research.Id;
        }

        private IQueryable<KeywordResearchTarget> ApplyFilter(IQueryable<KeywordResearchTarget> targets, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return targets;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "research" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    targets = searchField.Name switch
                    {
                        "id" => targets.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => targets.Where(t => t.Name.Contains(searchField.Value)),
                        "research" => targets.Where(t => t.Research.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    targets = targets.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Name.Contains(searchString)
                        || t.Research.Name.Contains(searchString));
                }
            }

            return targets;
        }

        private IQueryable<KeywordResearchTarget> ApplySorting(IQueryable<KeywordResearchTarget> targets, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                targets = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Id) : targets.OrderByDescending(t => t.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Name) : targets.OrderByDescending(t => t.Name),
                    "research" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Research.Name) : targets.OrderByDescending(t => t.Research.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return targets;
        }

        private IQueryable<KeywordResearchTarget> ApplyPaging(IQueryable<KeywordResearchTarget> targets, ListRequest request)
        {
            return targets.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
