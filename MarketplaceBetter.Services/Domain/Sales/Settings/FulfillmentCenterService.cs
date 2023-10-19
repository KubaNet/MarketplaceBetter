using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales.Settings;
using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Sales.Settings.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales.Settings
{
    public class FulfillmentCenterService : IFulfillmentCenterService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FulfillmentCenter> _repository;

        public FulfillmentCenterService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<FulfillmentCenter>();
        }

        public FulfillmentCenterModel Get(long id) => _mapper.Map<FulfillmentCenterModel>(_repository.Get(id));

        public FulfillmentCenterModel GetFor(string code) => _mapper.Map<FulfillmentCenterModel>(_repository.SingleOrDefault(c => c.Code.Equals(code)));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<FulfillmentCenter> centers = _repository.GetQuery();

            centers = ApplyFilter(centers, request);

            return centers.Count();
        }

        public IList<FulfillmentCenterModel> GetForListRequest(ListRequest request)
        {
            IQueryable<FulfillmentCenter> centers = _repository.GetQuery();

            centers = ApplyFilter(centers, request);
            centers = ApplySorting(centers, request);
            centers = ApplyPaging(centers, request);

            return _mapper.Map<IList<FulfillmentCenterModel>>(centers);
        }

        public void Add(FulfillmentCenterModel center)
        {
            FulfillmentCenter centerToAdd = new();

            TransferValues(centerToAdd, center);

            _repository.Add(centerToAdd);
            _unitOfWork.Save();
        }

        public void Update(FulfillmentCenterModel center)
        {
            FulfillmentCenter centerToUpdate = _repository.Get(center.Id);

            TransferValues(centerToUpdate, center);

            _repository.Update(centerToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(FulfillmentCenter toCenter, FulfillmentCenterModel fromCenter)
        {
            toCenter.Code = fromCenter.Code;
            toCenter.CountryId = fromCenter.Country.Id;
        }

        private IQueryable<FulfillmentCenter> ApplyFilter(IQueryable<FulfillmentCenter> centers, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return centers;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "code", "country" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    centers = searchField.Name switch
                    {
                        "id" => centers.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "code" => centers.Where(c => c.Code.Contains(searchField.Value)),
                        "country" => centers.Where(c => c.Country.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    centers = centers.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Code.Contains(searchString)
                        || c.Country.Name.Contains(searchString));
                }
            }

            return centers;
        }

        private IQueryable<FulfillmentCenter> ApplySorting(IQueryable<FulfillmentCenter> centers, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                centers = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? centers.OrderBy(c => c.Id) : centers.OrderByDescending(c => c.Id),
                    "code" => request.SortDirection == SortDirection.Ascending ? centers.OrderBy(c => c.Code) : centers.OrderByDescending(c => c.Code),
                    "country" => request.SortDirection == SortDirection.Ascending ? centers.OrderBy(c => c.Country.Name) : centers.OrderByDescending(c => c.Country.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                centers = centers.OrderBy(c => c.Id);
            }

            return centers;
        }

        private IQueryable<FulfillmentCenter> ApplyPaging(IQueryable<FulfillmentCenter> centers, ListRequest request)
        {
            return centers.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
