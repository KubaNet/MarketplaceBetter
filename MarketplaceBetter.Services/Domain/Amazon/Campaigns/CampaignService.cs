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
    public class CampaignService : ICampaignService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Campaign> _repository;
        private readonly IAdGroupService _adGroupService;
        private readonly IProductAdService _productAdService;

        public CampaignService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAdGroupService adGroupService,
            IProductAdService productAdService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Campaign>();
            _adGroupService = adGroupService;
            _productAdService = productAdService;
        }

        public CampaignModel Get(long id) => _mapper.Map<CampaignModel>(_repository.Get(id));

        public IList<CampaignModel> GetAll() => _mapper.Map<IList<CampaignModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<CampaignModel> GetForInstance(long instanceId) => _mapper.Map<IList<CampaignModel>>(_repository.GetQuery().Where(c => c.InstanceId == instanceId).OrderBy(g => g.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Campaign> campaigns = _repository.GetQuery();

            ApplyFilter(campaigns, request);

            return campaigns.Count();
        }

        public IList<CampaignModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Campaign> campaigns = _repository.GetQuery();

            campaigns = ApplyFilter(campaigns, request);
            campaigns = ApplySorting(campaigns, request);
            campaigns = ApplyPaging(campaigns, request);

            return _mapper.Map<IList<CampaignModel>>(campaigns);
        }

        public void Add(CampaignModel campaign)
        {
            Campaign campaignToAdd = new();

            TransferValues(campaignToAdd, campaign);

            _repository.Add(campaignToAdd);
            _unitOfWork.Save();

            long adGroupId = _adGroupService.AddForCampaign(campaignToAdd.Id);
            _productAdService.AddForAdGroup(adGroupId, campaign.Product.Id);
        }

        public void Update(CampaignModel campaign)
        {
            Campaign campaignToUpdate = _repository.Get(campaign.Id);

            TransferValues(campaignToUpdate, campaign);

            _repository.Update(campaignToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Campaign toCampaign, CampaignModel fromCampaign)
        {
            toCampaign.Name = fromCampaign.Name;
            toCampaign.InstanceId = fromCampaign.Instance.Id;
            toCampaign.TypeId = fromCampaign.Type.Id;
            toCampaign.StrategyId = fromCampaign.Strategy.Id;
            toCampaign.ProductId = fromCampaign.Product.Id;
        }

        private IQueryable<Campaign> ApplyFilter(IQueryable<Campaign> campaigns, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return campaigns;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "instance", "product", "brand"};
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    campaigns = searchField.Name switch
                    {
                        "id" => campaigns.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => campaigns.Where(c => c.Name.Contains(searchField.Value)),
                        "product" => campaigns.Where(c => c.Product.Name.Contains(searchField.Value)),
                        "brand" => campaigns.Where(c => c.Product.Brand.Name.Contains(searchField.Value)),
                        "instance" => campaigns.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    campaigns = campaigns.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Name.Contains(searchString)
                        || c.Product.Name.Contains(searchString)
                        || c.Product.Brand.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString));
                }
            }

            return campaigns;
        }

        private IQueryable<Campaign> ApplySorting(IQueryable<Campaign> campaigns, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                campaigns = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? campaigns.OrderBy(c => c.Id) : campaigns.OrderByDescending(c => c.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? campaigns.OrderBy(c => c.Name) : campaigns.OrderByDescending(c => c.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? campaigns.OrderBy(c => c.Product.Name) : campaigns.OrderByDescending(c => c.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? campaigns.OrderBy(c => c.Product.Brand.Name) : campaigns.OrderByDescending(c => c.Product.Brand.Name),
                    "instance" => request.SortDirection == SortDirection.Ascending ? campaigns.OrderBy(c => c.Instance.Name) : campaigns.OrderByDescending(c => c.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return campaigns;
        }

        private IQueryable<Campaign> ApplyPaging(IQueryable<Campaign> campaigns, ListRequest request)
        {
            return campaigns.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
