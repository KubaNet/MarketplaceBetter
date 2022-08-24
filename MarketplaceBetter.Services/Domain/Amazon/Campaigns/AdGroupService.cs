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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class AdGroupService : IAdGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AdGroup> _repository;
        private readonly IRepository<AdEntityStatus> _statusRepository;

        public AdGroupService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AdGroup>();
            _statusRepository = unitOfWork.GetRepository<AdEntityStatus>();
        }

        public AdGroupModel Get(long id) => _mapper.Map<AdGroupModel>(_repository.Get(id));

        public AdGroupModel GetForCampaign(long campaignId) => _mapper.Map<AdGroupModel>(_repository.SingleOrDefault(g => g.CampaignId == campaignId));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AdGroup> adGroups = _repository.GetQuery();

            ApplyFilter(adGroups, request);

            return adGroups.Count();
        }

        public IList<AdGroupModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AdGroup> adGroups = _repository.GetQuery();

            adGroups = ApplyFilter(adGroups, request);
            adGroups = ApplySorting(adGroups, request);
            adGroups = ApplyPaging(adGroups, request);

            return _mapper.Map<IList<AdGroupModel>>(adGroups);
        }

        public long AddForCampaign(long campaignId)
        {
            AdGroup adGroup = new AdGroup { Name = $"Ad_Group_{GetNextNumber()}", CampaignId = campaignId, 
                Status = _statusRepository.Single(s => s.SystemName == AdEntityStatusEnum.Enabled) };

            _repository.Add(adGroup);
            _unitOfWork.Save();

            return adGroup.Id;
        }

        public void Update(AdGroupModel adGroup)
        {
            AdGroup adGroupToUpdate = _repository.Get(adGroup.Id);

            TransferValues(adGroupToUpdate, adGroup);

            _repository.Update(adGroupToUpdate);
            _unitOfWork.Save();
        }

        private long GetNextNumber()
        {
            AdGroup lastAdGroup = _repository.GetQuery().OrderBy(g => g.Id).LastOrDefault();
            long nextNumber = lastAdGroup != null ? lastAdGroup.Id + 1 : 1;

            return nextNumber;
        }

        private void TransferValues(AdGroup toAdGroup, AdGroupModel fromAdGroup)
        {
            toAdGroup.AmazonId = fromAdGroup.AmazonId;
        }

        private IQueryable<AdGroup> ApplyFilter(IQueryable<AdGroup> adGroups, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return adGroups;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "amazonid", "status", "campaign" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    adGroups = searchField.Name switch
                    {
                        "id" => adGroups.Where(g => g.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => adGroups.Where(g => g.Name.Contains(searchField.Value)),
                        "amazonid" => adGroups.Where(g => g.AmazonId.Contains(searchField.Value)),
                        "status" => adGroups.Where(g => g.Status.Name.Contains(searchField.Value)),
                        "campaign" => adGroups.Where(g => g.Campaign.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    adGroups = adGroups.Where(g => g.Id == searchString.ParseToIntOrDefault()
                        || g.Name.Contains(searchString)
                        || g.AmazonId.Contains(searchString)
                        || g.Status.Name.Contains(searchString)
                        || g.Campaign.Name.Contains(searchString));
                }
            }

            return adGroups;
        }

        private IQueryable<AdGroup> ApplySorting(IQueryable<AdGroup> adGroups, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                adGroups = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? adGroups.OrderBy(g => g.Id) : adGroups.OrderByDescending(g => g.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? adGroups.OrderBy(g => g.Name) : adGroups.OrderByDescending(g => g.Name),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? adGroups.OrderBy(g => g.AmazonId) : adGroups.OrderByDescending(g => g.AmazonId),
                    "status" => request.SortDirection == SortDirection.Ascending ? adGroups.OrderBy(g => g.Status.Name) : adGroups.OrderByDescending(g => g.Status.Name),
                    "campaign" => request.SortDirection == SortDirection.Ascending ? adGroups.OrderBy(g => g.Campaign.Name) : adGroups.OrderByDescending(g => g.Campaign.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return adGroups;
        }

        private IQueryable<AdGroup> ApplyPaging(IQueryable<AdGroup> adGroups, ListRequest request)
        {
            return adGroups.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
