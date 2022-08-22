using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class NegativeKeywordService : INegativeKeywordService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<NegativeKeyword> _repository;
        private readonly IRepository<AdEntityStatus> _adEntityStatusRepository;

        public NegativeKeywordService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<NegativeKeyword>();
            _adEntityStatusRepository = unitOfWork.GetRepository<AdEntityStatus>();
        }

        public NegativeKeywordModel Get(long id) => _mapper.Map<NegativeKeywordModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<NegativeKeyword> negativeKeywords = _repository.GetQuery();

            ApplyFilter(negativeKeywords, request);

            return negativeKeywords.Count();
        }

        public IList<NegativeKeywordModel> GetForListRequest(ListRequest request)
        {
            IQueryable<NegativeKeyword> negativeKeywords = _repository.GetQuery();

            negativeKeywords = ApplyFilter(negativeKeywords, request);
            negativeKeywords = ApplySorting(negativeKeywords, request);
            negativeKeywords = ApplyPaging(negativeKeywords, request);

            return _mapper.Map<IList<NegativeKeywordModel>>(negativeKeywords);
        }

        public void Add(NegativeKeywordModel negativeKeyword)
        {
            NegativeKeyword negativeKeywordToAdd = new();

            TransferValues(negativeKeywordToAdd, negativeKeyword);

            negativeKeywordToAdd.Status = _adEntityStatusRepository.Single(s => s.SystemName == AdEntityStatusEnum.Enabled);

            _repository.Add(negativeKeywordToAdd);
            _unitOfWork.Save();
        }

        public void Update(NegativeKeywordModel negativeKeyword)
        {
            NegativeKeyword negativeKeywordToUpdate = _repository.Get(negativeKeyword.Id);

            TransferValues(negativeKeywordToUpdate, negativeKeyword);

            _repository.Update(negativeKeywordToUpdate);
            _unitOfWork.Save();
        }

        public void UpdateStatus(long negativeKeywordId, AdEntityStatusEnum status)
        {
            NegativeKeyword negativeKeyword = _repository.Get(negativeKeywordId);
            AdEntityStatus newStatus = _adEntityStatusRepository.Single(s => s.SystemName == status);

            negativeKeyword.Status = newStatus;

            _repository.Update(negativeKeyword);
            _unitOfWork.Save();
        }

        private void TransferValues(NegativeKeyword toNegativeKeyword, NegativeKeywordModel fromNegativeKeyword)
        {
            toNegativeKeyword.AmazonId = fromNegativeKeyword.AmazonId;
        }

        private IQueryable<NegativeKeyword> ApplyFilter(IQueryable<NegativeKeyword> negativeKeywords, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return negativeKeywords;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "keyword", "matchtype", "amazonid", "status", "campaign" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    negativeKeywords = searchField.Name switch
                    {
                        "id" => negativeKeywords.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "keyword" => negativeKeywords.Where(t => t.Keyword.Contains(searchField.Value)),
                        "matchtype" => negativeKeywords.Where(t => t.MatchType.Name.Contains(searchField.Value)),
                        "amazonid" => negativeKeywords.Where(t => t.AmazonId.Contains(searchField.Value)),
                        "status" => negativeKeywords.Where(t => t.Status.Name.Contains(searchField.Value)),
                        "campaign" => negativeKeywords.Where(t => t.AdGroup.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    negativeKeywords = negativeKeywords.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Keyword.Contains(searchString)
                        || t.MatchType.Name.Contains(searchString)
                        || t.AmazonId.Contains(searchString)
                        || t.Status.Name.Contains(searchString)
                        || t.AdGroup.Campaign.Name.Contains(searchString));
                }
            }

            return negativeKeywords;
        }

        private IQueryable<NegativeKeyword> ApplySorting(IQueryable<NegativeKeyword> negativeKeywords, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                negativeKeywords = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.Id) : negativeKeywords.OrderByDescending(t => t.Id),
                    "keyword" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.Keyword) : negativeKeywords.OrderByDescending(t => t.Keyword),
                    "matchtype" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.MatchType.Name) : negativeKeywords.OrderByDescending(t => t.MatchType.Name),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.AmazonId) : negativeKeywords.OrderByDescending(t => t.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.Status.Name) : negativeKeywords.OrderByDescending(t => t.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(t => t.AdGroup.Campaign.Name) : negativeKeywords.OrderByDescending(t => t.AdGroup.Campaign.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return negativeKeywords;
        }

        private IQueryable<NegativeKeyword> ApplyPaging(IQueryable<NegativeKeyword> negativeKeywords, ListRequest request)
        {
            return negativeKeywords.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
