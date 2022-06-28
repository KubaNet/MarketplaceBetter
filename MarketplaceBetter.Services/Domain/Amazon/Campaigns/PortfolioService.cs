using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.CsvRecords;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.CsvMaps;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Portfolio> _repository;

        public PortfolioService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAdGroupService adGroupService,
            IProductAdService productAdService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Portfolio>();
        }

        public PortfolioModel Get(long id) => _mapper.Map<PortfolioModel>(_repository.Get(id));

        public IList<PortfolioModel> GetForInstance(long instanceId) => _mapper.Map<IList<PortfolioModel>>(_repository.GetQuery().Where(p => p.InstanceId == instanceId).OrderBy(g => g.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Portfolio> portfolios = _repository.GetQuery();

            ApplyFilter(portfolios, request);

            return portfolios.Count();
        }

        public IList<PortfolioModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Portfolio> portfolios = _repository.GetQuery();

            portfolios = ApplyFilter(portfolios, request);
            portfolios = ApplySorting(portfolios, request);
            portfolios = ApplyPaging(portfolios, request);

            return _mapper.Map<IList<PortfolioModel>>(portfolios);
        }

        public void Add(PortfolioModel portfolio)
        {
            Portfolio portfolioToAdd = new();

            TransferValues(portfolioToAdd, portfolio);

            _repository.Add(portfolioToAdd);
            _unitOfWork.Save();
        }

        public void Update(PortfolioModel portfolio)
        {
            Portfolio portfolioToUpdate = _repository.Get(portfolio.Id);

            TransferValues(portfolioToUpdate, portfolio);

            _repository.Update(portfolioToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Portfolio toPortfolio, PortfolioModel fromPortfolio)
        {
            toPortfolio.InstanceId = fromPortfolio.Instance.Id;
            toPortfolio.Name = fromPortfolio.Name;
            toPortfolio.AmazonId = fromPortfolio.AmazonId;
        }

        private IQueryable<Portfolio> ApplyFilter(IQueryable<Portfolio> portfolios, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return portfolios;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "amazonid", "instance"};
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    portfolios = searchField.Name switch
                    {
                        "id" => portfolios.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => portfolios.Where(p => p.Name.Contains(searchField.Value)),
                        "amazonid" => portfolios.Where(p => p.AmazonId.Contains(searchField.Value)),
                        "instance" => portfolios.Where(p => p.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    portfolios = portfolios.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Name.Contains(searchString)
                        || p.AmazonId.Contains(searchString)
                        || p.Instance.Name.Contains(searchString));
                }
            }

            return portfolios;
        }

        private IQueryable<Portfolio> ApplySorting(IQueryable<Portfolio> portfolios, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                portfolios = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.Id) : portfolios.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.Name) : portfolios.OrderByDescending(p => p.Name),
                    "amazonid" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.AmazonId) : portfolios.OrderByDescending(p => p.AmazonId),
                    "instance" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.Instance.Name) : portfolios.OrderByDescending(p => p.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return portfolios;
        }

        private IQueryable<Portfolio> ApplyPaging(IQueryable<Portfolio> portfolios, ListRequest request)
        {
            return portfolios.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
