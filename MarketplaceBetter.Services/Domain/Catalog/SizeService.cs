using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MarketplaceBetter.Domain.Entities.Catalog.Size> _repository;
        private readonly IMapper _mapper;

        public SizeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<MarketplaceBetter.Domain.Entities.Catalog.Size>();
            _mapper = mapper;
        }

        public SizeModel Get(long id) => _mapper.Map<SizeModel>(_repository.Get(id));

        public IList<SizeModel> GetAll() => _mapper.Map<IList<SizeModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<SizeModel> GetAllForGroup(long groupId) => _mapper.Map<IList<SizeModel>>(_repository.GetQuery().Where(s => s.GroupId == groupId));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> sizes = _repository.GetQuery();

            ApplyFilter(sizes, request);

            return sizes.Count();
        }

        public IList<SizeModel> GetForListRequest(ListRequest request)
        {
            IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> sizes = _repository.GetQuery();

            sizes = ApplyFilter(sizes, request);
            sizes = ApplySorting(sizes, request);
            sizes = ApplyPaging(sizes, request);

            return _mapper.Map<IList<SizeModel>>(sizes);
        }

        public void Add(SizeModel size)
        {
            MarketplaceBetter.Domain.Entities.Catalog.Size sizeToAdd = new();

            TransferValues(sizeToAdd, size);

            _repository.Add(sizeToAdd);
            _unitOfWork.Save();
        }

        public void Update(SizeModel size)
        {
            MarketplaceBetter.Domain.Entities.Catalog.Size sizeToUpdate = _repository.Get(size.Id);

            TransferValues(sizeToUpdate, size);

            _repository.Update(sizeToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(MarketplaceBetter.Domain.Entities.Catalog.Size toSize, SizeModel fromSize)
        {
            toSize.Name = fromSize.Name;
            toSize.Code = fromSize.Code;
            toSize.GroupId = fromSize.Group.Id;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> ApplyFilter(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> sizes, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return sizes;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "code", "brand", "group" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    sizes = searchField.Name switch
                    {
                        "id" => sizes.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => sizes.Where(s => s.Name.Contains(searchField.Value)),
                        "code" => sizes.Where(s => s.Code.Contains(searchField.Value)),
                        "brand" => sizes.Where(s => s.Group.Brand.Name.Contains(searchField.Value)),
                        "group" => sizes.Where(s => s.Group.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    sizes = sizes.Where(s => s.Id == searchString.ParseToIntOrDefault()
                        || s.Name.Contains(searchString)
                        || s.Code.Contains(searchString)
                        || s.Group.Brand.Name.Contains(searchString)
                        || s.Group.Name.Contains(searchString));
                }
            }

            return sizes;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> ApplySorting(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> sizes, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                sizes = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Id) : sizes.OrderByDescending(s => s.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Name) : sizes.OrderByDescending(s => s.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Code) : sizes.OrderByDescending(s => s.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Group.Brand.Name) : sizes.OrderByDescending(s => s.Group.Brand.Name),
                    "group" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Group.Name) : sizes.OrderByDescending(s => s.Group.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return sizes;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> ApplyPaging(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Size> sizes, ListRequest request)
        {
            return sizes.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
