using AutoMapper;
using MarketplaceBetter.Domain.Entities.Advertising.Campaigns;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Advertising.Campaigns;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Advertising.Campaigns.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceBetter.Services.Domain.Advertising.Campaigns
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Portfolio> _repository;
        private readonly IUserService _userService;

        public PortfolioService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Portfolio>();
            _userService = userService;
        }

        public PortfolioModel Get(long id) => _mapper.Map<PortfolioModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Portfolio> portfolios = _repository.GetQuery();

            portfolios = ApplyFilter(portfolios, request);

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
            toPortfolio.Name = fromPortfolio.Name;
            toPortfolio.AmazonId = fromPortfolio.AmazonId;
            toPortfolio.InstanceId = fromPortfolio.Instance.Id;
        }

        private IQueryable<Portfolio> ApplyFilter(IQueryable<Portfolio> portfolios, ListRequest request)
        {
            if (_userService.IsSpecificInstance())
            {
                InstanceModel currentInstance = _userService.GetCurrentInstance();
                portfolios = portfolios.Where(p => p.InstanceId == currentInstance.Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return portfolios;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "amazon_id", "instance" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    portfolios = searchField.Name switch
                    {
                        "id" => portfolios.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => portfolios.Where(p => p.Name.Contains(searchField.Value)),
                        "amazon_id" => portfolios.Where(p => p.AmazonId.Contains(searchField.Value)),
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
                    "amazon_id" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.AmazonId) : portfolios.OrderByDescending(p => p.AmazonId),
                    "instance" => request.SortDirection == SortDirection.Ascending ? portfolios.OrderBy(p => p.Instance.Name) : portfolios.OrderByDescending(p => p.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                portfolios = portfolios.OrderBy(p => p.Name);
            }

            return portfolios;
        }

        private IQueryable<Portfolio> ApplyPaging(IQueryable<Portfolio> portfolios, ListRequest request)
        {
            return portfolios.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
