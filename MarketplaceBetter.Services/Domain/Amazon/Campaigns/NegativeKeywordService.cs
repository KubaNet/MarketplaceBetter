using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        private readonly IRepository<AdGroup> _adGroupRepository;

        public NegativeKeywordService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<NegativeKeyword>();
            _adEntityStatusRepository = unitOfWork.GetRepository<AdEntityStatus>();
            _adGroupRepository = unitOfWork.GetRepository<AdGroup>();
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

        public void Add(NegativeKeywordModel negativeKeyword, CampaignModel campaign)
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
            toNegativeKeyword.AdGroupId = fromNegativeKeyword.AdGroup.Id;
            toNegativeKeyword.Keyword = fromNegativeKeyword.Keyword;
            toNegativeKeyword.MatchTypeId = fromNegativeKeyword.MatchType.Id;
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
                        "id" => negativeKeywords.Where(k => k.Id == searchField.Value.ParseToIntOrDefault()),
                        "keyword" => negativeKeywords.Where(k => k.Keyword.Contains(searchField.Value)),
                        "matchtype" => negativeKeywords.Where(k => k.MatchType.Name.Contains(searchField.Value)),
                        "amazonid" => negativeKeywords.Where(k => k.AmazonId.Contains(searchField.Value)),
                        "status" => negativeKeywords.Where(k => k.Status.Name.Contains(searchField.Value)),
                        "campaign" => negativeKeywords.Where(k => k.AdGroup.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    negativeKeywords = negativeKeywords.Where(k => k.Id == searchString.ParseToIntOrDefault()
                        || k.Keyword.Contains(searchString)
                        || k.MatchType.Name.Contains(searchString)
                        || k.AmazonId.Contains(searchString)
                        || k.Status.Name.Contains(searchString)
                        || k.AdGroup.Campaign.Name.Contains(searchString));
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
                    "id" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.Id) : negativeKeywords.OrderByDescending(k => k.Id),
                    "keyword" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.Keyword) : negativeKeywords.OrderByDescending(k => k.Keyword),
                    "matchtype" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.MatchType.Name) : negativeKeywords.OrderByDescending(k => k.MatchType.Name),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.AmazonId) : negativeKeywords.OrderByDescending(k => k.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.Status.Name) : negativeKeywords.OrderByDescending(k => k.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? negativeKeywords.OrderBy(k => k.AdGroup.Campaign.Name) : negativeKeywords.OrderByDescending(k => k.AdGroup.Campaign.Name),
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
