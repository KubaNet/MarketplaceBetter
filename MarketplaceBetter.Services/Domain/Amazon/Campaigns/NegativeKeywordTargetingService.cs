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
    public class NegativeKeywordTargetingService : INegativeKeywordTargetingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<NegativeKeywordTargeting> _repository;
        private readonly IRepository<AdEntityStatus> _adEntityStatusRepository;

        public NegativeKeywordTargetingService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<NegativeKeywordTargeting>();
            _adEntityStatusRepository = unitOfWork.GetRepository<AdEntityStatus>();
        }

        public NegativeKeywordTargetingModel Get(long id) => _mapper.Map<NegativeKeywordTargetingModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<NegativeKeywordTargeting> negativeKeywordTargetings = _repository.GetQuery();

            ApplyFilter(negativeKeywordTargetings, request);

            return negativeKeywordTargetings.Count();
        }

        public IList<NegativeKeywordTargetingModel> GetForListRequest(ListRequest request)
        {
            IQueryable<NegativeKeywordTargeting> negativeKeywordTargetings = _repository.GetQuery();

            negativeKeywordTargetings = ApplyFilter(negativeKeywordTargetings, request);
            negativeKeywordTargetings = ApplySorting(negativeKeywordTargetings, request);
            negativeKeywordTargetings = ApplyPaging(negativeKeywordTargetings, request);

            return _mapper.Map<IList<NegativeKeywordTargetingModel>>(negativeKeywordTargetings);
        }

        public void Update(NegativeKeywordTargetingModel negativeKeywordTargeting)
        {
            NegativeKeywordTargeting negativeKeywordTargetingToUpdate = _repository.Get(negativeKeywordTargeting.Id);

            TransferValues(negativeKeywordTargetingToUpdate, negativeKeywordTargeting);

            _repository.Update(negativeKeywordTargetingToUpdate);
            _unitOfWork.Save();
        }

        public void UpdateStatus(long negativeKeywordTargetingId, AdEntityStatusEnum status)
        {
            NegativeKeywordTargeting negativeKeywordTargeting = _repository.Get(negativeKeywordTargetingId);
            AdEntityStatus newStatus = _adEntityStatusRepository.Single(s => s.SystemName == status);

            negativeKeywordTargeting.Status = newStatus;

            _repository.Update(negativeKeywordTargeting);
            _unitOfWork.Save();
        }

        private void TransferValues(NegativeKeywordTargeting toNegativeKeywordTargeting, NegativeKeywordTargetingModel fromNegativeKeywordTargeting)
        {
            toNegativeKeywordTargeting.AmazonId = fromNegativeKeywordTargeting.AmazonId;
        }

        private IQueryable<NegativeKeywordTargeting> ApplyFilter(IQueryable<NegativeKeywordTargeting> negativeKeywordTargetings, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return negativeKeywordTargetings;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "keyword", "matchtype", "amazonid", "status", "campaign" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    negativeKeywordTargetings = searchField.Name switch
                    {
                        "id" => negativeKeywordTargetings.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "keyword" => negativeKeywordTargetings.Where(t => t.Keyword.Contains(searchField.Value)),
                        "matchtype" => negativeKeywordTargetings.Where(t => t.MatchType.Name.Contains(searchField.Value)),
                        "amazonid" => negativeKeywordTargetings.Where(t => t.AmazonId.Contains(searchField.Value)),
                        "status" => negativeKeywordTargetings.Where(t => t.Status.Name.Contains(searchField.Value)),
                        "campaign" => negativeKeywordTargetings.Where(t => t.AdGroup.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    negativeKeywordTargetings = negativeKeywordTargetings.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Keyword.Contains(searchString)
                        || t.MatchType.Name.Contains(searchString)
                        || t.AmazonId.Contains(searchString)
                        || t.Status.Name.Contains(searchString)
                        || t.AdGroup.Campaign.Name.Contains(searchString));
                }
            }

            return negativeKeywordTargetings;
        }

        private IQueryable<NegativeKeywordTargeting> ApplySorting(IQueryable<NegativeKeywordTargeting> negativeKeywordTargetings, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                negativeKeywordTargetings = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.Id) : negativeKeywordTargetings.OrderByDescending(t => t.Id),
                    "keyword" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.Keyword) : negativeKeywordTargetings.OrderByDescending(t => t.Keyword),
                    "matchtype" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.MatchType.Name) : negativeKeywordTargetings.OrderByDescending(t => t.MatchType.Name),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.AmazonId) : negativeKeywordTargetings.OrderByDescending(t => t.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.Status.Name) : negativeKeywordTargetings.OrderByDescending(t => t.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? negativeKeywordTargetings.OrderBy(t => t.AdGroup.Campaign.Name) : negativeKeywordTargetings.OrderByDescending(t => t.AdGroup.Campaign.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return negativeKeywordTargetings;
        }

        private IQueryable<NegativeKeywordTargeting> ApplyPaging(IQueryable<NegativeKeywordTargeting> negativeKeywordTargetings, ListRequest request)
        {
            return negativeKeywordTargetings.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
