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
    public class AmazonCampaignService : IAmazonCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonCampaign> _repository;
        private readonly IMapper _mapper;

        public AmazonCampaignService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonCampaign>();
            _mapper = mapper;
        }

        public AmazonCampaignModel Get(long id) => _mapper.Map<AmazonCampaignModel>(_repository.Get(id));

        public IList<AmazonCampaignModel> GetAll() => _mapper.Map<IList<AmazonCampaignModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<AmazonCampaignModel> GetForInstance(long instanceId) => _mapper.Map<IList<AmazonCampaignModel>>(_repository.GetQuery().Where(c => c.InstanceId == instanceId).OrderBy(g => g.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<AmazonCampaign> campaigns = _repository.GetQuery();

            ApplyFilter(campaigns, request);

            return campaigns.Count();
        }

        public IList<AmazonCampaignModel> GetForListRequest(ListRequest request)
        {
            IQueryable<AmazonCampaign> campaigns = _repository.GetQuery();

            campaigns = ApplyFilter(campaigns, request);
            campaigns = ApplySorting(campaigns, request);
            campaigns = ApplyPaging(campaigns, request);

            return _mapper.Map<IList<AmazonCampaignModel>>(campaigns);
        }

        public void Add(AmazonCampaignModel campaign)
        {
            AmazonCampaign campaignToAdd = new();

            TransferValues(campaignToAdd, campaign);

            _repository.Add(campaignToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonCampaignModel campaign)
        {
            AmazonCampaign campaignToUpdate = _repository.Get(campaign.Id);

            TransferValues(campaignToUpdate, campaign);

            _repository.Update(campaignToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonCampaign toCampaign, AmazonCampaignModel fromCampaign)
        {
            toCampaign.Name = fromCampaign.Name;
            toCampaign.ProductId = fromCampaign.Product.Id;
            toCampaign.InstanceId = fromCampaign.Instance.Id;
        }

        private IQueryable<AmazonCampaign> ApplyFilter(IQueryable<AmazonCampaign> products, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return products;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "instance", "product", "brand"};
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    products = searchField.Name switch
                    {
                        "id" => products.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => products.Where(c => c.Name.Contains(searchField.Value)),
                        "product" => products.Where(c => c.Product.Name.Contains(searchField.Value)),
                        "brand" => products.Where(c => c.Product.Brand.Name.Contains(searchField.Value)),
                        "instance" => products.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    products = products.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Name.Contains(searchString)
                        || c.Product.Name.Contains(searchString)
                        || c.Product.Brand.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString));
                }
            }

            return products;
        }

        private IQueryable<AmazonCampaign> ApplySorting(IQueryable<AmazonCampaign> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Id) : products.OrderByDescending(c => c.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Name) : products.OrderByDescending(c => c.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Product.Name) : products.OrderByDescending(c => c.Product.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Product.Brand.Name) : products.OrderByDescending(c => c.Product.Brand.Name),
                    "instance" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(c => c.Instance.Name) : products.OrderByDescending(c => c.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<AmazonCampaign> ApplyPaging(IQueryable<AmazonCampaign> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
