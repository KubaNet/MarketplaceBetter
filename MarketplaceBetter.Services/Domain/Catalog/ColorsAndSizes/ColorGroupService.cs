using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class ColorGroupService : IColorGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ColorGroup> _repository;
        private readonly IMapper _mapper;

        public ColorGroupService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ColorGroup>();
            _mapper = mapper;
        }

        public ColorGroupModel Get(long id) => _mapper.Map<ColorGroupModel>(_repository.Get(id));

        public IList<ColorGroupModel> GetAll() => _mapper.Map<IList<ColorGroupModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<ColorGroupModel> GetAllForBrand(long brandId) => _mapper.Map<IList<ColorGroupModel>>(_repository.Where(g => g.BrandId == brandId).OrderBy(g => g.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ColorGroup> groups = _repository.GetQuery();

            ApplyFilter(groups, request);

            return groups.Count();
        }

        public IList<ColorGroupModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ColorGroup> groups = _repository.GetQuery();

            groups = ApplyFilter(groups, request);
            groups = ApplySorting(groups, request);
            groups = ApplyPaging(groups, request);

            return _mapper.Map<IList<ColorGroupModel>>(groups);
        }

        public void Add(ColorGroupModel group)
        {
            ColorGroup groupToAdd = new();

            TransferValues(groupToAdd, group);

            _repository.Add(groupToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorGroupModel group)
        {
            ColorGroup groupToUpdate = _repository.Get(group.Id);

            TransferValues(groupToUpdate, group);

            _repository.Update(groupToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ColorGroup toGroup, ColorGroupModel fromGroup)
        {
            toGroup.Name = fromGroup.Name;
            toGroup.BrandId = fromGroup.Brand.Id;
        }

        private IQueryable<ColorGroup> ApplyFilter(IQueryable<ColorGroup> groups, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return groups;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    groups = searchField.Name switch
                    {
                        "id" => groups.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => groups.Where(p => p.Name.Contains(searchField.Value)),
                        "brand" => groups.Where(p => p.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    groups = groups.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Name.Contains(searchString)
                        || p.Brand.Name.Contains(searchString));
                }
            }

            return groups;
        }

        private IQueryable<ColorGroup> ApplySorting(IQueryable<ColorGroup> groups, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                groups = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(p => p.Id) : groups.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(p => p.Name) : groups.OrderByDescending(p => p.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(p => p.Brand.Name) : groups.OrderByDescending(p => p.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return groups;
        }

        private IQueryable<ColorGroup> ApplyPaging(IQueryable<ColorGroup> groups, ListRequest request)
        {
            return groups.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
