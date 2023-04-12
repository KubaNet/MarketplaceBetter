using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Settings.Interfaces;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Size;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class SizeService : ISizeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Size> _repository;
        private readonly IUserService _userService;

        public SizeService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Size>();
            _userService = userService;
        }

        public SizeModel Get(long id) => _mapper.Map<SizeModel>(_repository.Get(id));

        public IList<SizeModel> GetAll() => _mapper.Map<IList<SizeModel>>(_repository.GetQuery().OrderBy(s => s.Id));

        public IList<SizeModel> GetAllForGroup(long groupId) => _mapper.Map<IList<SizeModel>>(_repository.GetQuery().Where(s => s.GroupId == groupId).OrderBy(s => s.Id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Size> sizes = _repository.GetQuery();

            sizes = ApplyFilter(sizes, request);

            return sizes.Count();
        }

        public IList<SizeModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Size> sizes = _repository.GetQuery();

            sizes = ApplyFilter(sizes, request);
            sizes = ApplySorting(sizes, request);
            sizes = ApplyPaging(sizes, request);

            return _mapper.Map<IList<SizeModel>>(sizes);
        }

        public void Add(SizeModel size)
        {
            Size sizeToAdd = new();

            TransferValues(sizeToAdd, size);

            _repository.Add(sizeToAdd);
            _unitOfWork.Save();
        }

        public void Update(SizeModel size)
        {
            Size sizeToUpdate = _repository.Get(size.Id);

            TransferValues(sizeToUpdate, size);

            _repository.Update(sizeToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Size toSize, SizeModel fromSize)
        {
            toSize.Name = fromSize.Name;
            toSize.Code = fromSize.Code;
            toSize.IsOneSize = fromSize.IsOneSize;
            toSize.GroupId = fromSize.Group.Id;
        }

        private IQueryable<Size> ApplyFilter(IQueryable<Size> sizes, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                sizes = sizes.Where(s => s.Group.BrandId == _userService.GetCurrentBrand().Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return sizes;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "code", "isonesize", "brand", "group" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    sizes = searchField.Name switch
                    {
                        "id" => sizes.Where(s => s.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => sizes.Where(s => s.Name.Contains(searchField.Value)),
                        "code" => sizes.Where(s => s.Code.Contains(searchField.Value)),
                        "isonesize" => searchField.Value == "true" ? sizes.Where(s => s.IsOneSize == true) : searchField.Value == "false" ? sizes.Where(s => s.IsOneSize == false) : sizes.Where(s => false),
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

        private IQueryable<Size> ApplySorting(IQueryable<Size> sizes, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                sizes = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Id) : sizes.OrderByDescending(s => s.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Name) : sizes.OrderByDescending(s => s.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Code) : sizes.OrderByDescending(s => s.Code),
                    "isonesize" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.IsOneSize) : sizes.OrderByDescending(s => s.IsOneSize),
                    "brand" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Group.Brand.Name) : sizes.OrderByDescending(s => s.Group.Brand.Name),
                    "group" => request.SortDirection == SortDirection.Ascending ? sizes.OrderBy(s => s.Group.Name) : sizes.OrderByDescending(s => s.Group.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return sizes;
        }

        private IQueryable<Size> ApplyPaging(IQueryable<Size> sizes, ListRequest request)
        {
            return sizes.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
