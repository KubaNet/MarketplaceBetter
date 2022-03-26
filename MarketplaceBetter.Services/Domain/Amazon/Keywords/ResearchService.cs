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

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class ResearchService : IResearchService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Research> _repository;

        public ResearchService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Research>();
        }

        public ResearchModel Get(long id) => _mapper.Map<ResearchModel>(_repository.Get(id));

        public IList<ResearchModel> GetAll() => _mapper.Map<IList<ResearchModel>>(_repository.GetQuery().OrderBy(r => r.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Research> researches = _repository.GetQuery();

            ApplyFilter(researches, request);

            return researches.Count();
        }

        public IList<ResearchModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Research> researches = _repository.GetQuery();

            researches = ApplyFilter(researches, request);
            researches = ApplySorting(researches, request);
            researches = ApplyPaging(researches, request);

            return _mapper.Map<IList<ResearchModel>>(researches);
        }

        public void Add(ResearchModel research)
        {
            Research researchToAdd = new();

            TransferValues(researchToAdd, research);

            _repository.Add(researchToAdd);
            _unitOfWork.Save();
        }

        public void Update(ResearchModel research)
        {
            Research researchToUpdate = _repository.Get(research.Id);

            TransferValues(researchToUpdate, research);

            _repository.Update(researchToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Research toResearch, ResearchModel fromResearch)
        {
            toResearch.Name = fromResearch.Name;
            toResearch.InstanceId = fromResearch.Instance.Id;
        }

        private IQueryable<Research> ApplyFilter(IQueryable<Research> researches, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return researches;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "instance" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    researches = searchField.Name switch
                    {
                        "id" => researches.Where(r => r.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => researches.Where(r => r.Name.Contains(searchField.Value)),
                        "instance" => researches.Where(r => r.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    researches = researches.Where(r => r.Id == searchString.ParseToIntOrDefault()
                        || r.Name.Contains(searchString)
                        || r.Instance.Name.Contains(searchString));
                }
            }

            return researches;
        }

        private IQueryable<Research> ApplySorting(IQueryable<Research> researches, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                researches = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? researches.OrderBy(r => r.Id) : researches.OrderByDescending(r => r.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? researches.OrderBy(r => r.Name) : researches.OrderByDescending(r => r.Name),
                    "instance" => request.SortDirection == SortDirection.Ascending ? researches.OrderBy(r => r.Instance.Name) : researches.OrderByDescending(r => r.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return researches;
        }

        private IQueryable<Research> ApplyPaging(IQueryable<Research> researches, ListRequest request)
        {
            return researches.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
