using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class SizeGroupService : ISizeGroupService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SizeGroup> _repository;
		private readonly ICurrentBrandService _currentBrandService;

		public SizeGroupService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
            ICurrentBrandService currentBrandService)
        {
			_mapper = mapper;
			_unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<SizeGroup>();
            _currentBrandService = currentBrandService;
        }

        public SizeGroupModel Get(long id) => _mapper.Map<SizeGroupModel>(_repository.Get(id));

        public IList<SizeGroupModel> GetAll() => _mapper.Map<IList<SizeGroupModel>>(_repository.GetQuery().OrderBy(g => g.Name));

        public IList<SizeGroupModel> GetAllForBrand(long brandId) => _mapper.Map<IList<SizeGroupModel>>(_repository.Where(g => g.BrandId == brandId).OrderBy(g => g.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<SizeGroup> groups = _repository.GetQuery();

            groups = ApplyFilter(groups, request);

            return groups.Count();
        }

        public IList<SizeGroupModel> GetForListRequest(ListRequest request)
        {
            IQueryable<SizeGroup> groups = _repository.GetQuery();

            groups = ApplyFilter(groups, request);
            groups = ApplySorting(groups, request);
            groups = ApplyPaging(groups, request);

            return _mapper.Map<IList<SizeGroupModel>>(groups);
        }

        public void Add(SizeGroupModel group)
        {
            SizeGroup groupToAdd = new();

            TransferValues(groupToAdd, group);

            _repository.Add(groupToAdd);
            _unitOfWork.Save();
        }

        public void Update(SizeGroupModel group)
        {
            SizeGroup groupToUpdate = _repository.Get(group.Id);

            TransferValues(groupToUpdate, group);

            _repository.Update(groupToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(SizeGroup toGroup, SizeGroupModel fromGroup)
        {
            toGroup.Name = fromGroup.Name;
            toGroup.BrandId = fromGroup.Brand.Id;
        }

        private IQueryable<SizeGroup> ApplyFilter(IQueryable<SizeGroup> groups, ListRequest request)
        {
			if (_currentBrandService.IsSpecificBrand())
			{
				groups = groups.Where(g => g.BrandId == _currentBrandService.GetCurrentBrand().Id);
			}

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
                        "id" => groups.Where(g => g.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => groups.Where(g => g.Name.Contains(searchField.Value)),
                        "brand" => groups.Where(g => g.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    groups = groups.Where(g => g.Id == searchString.ParseToIntOrDefault()
                        || g.Name.Contains(searchString)
                        || g.Brand.Name.Contains(searchString));
                }
            }

            return groups;
        }

        private IQueryable<SizeGroup> ApplySorting(IQueryable<SizeGroup> groups, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                groups = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(g => g.Id) : groups.OrderByDescending(g => g.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(g => g.Name) : groups.OrderByDescending(g => g.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? groups.OrderBy(g => g.Brand.Name) : groups.OrderByDescending(g => g.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return groups;
        }

        private IQueryable<SizeGroup> ApplyPaging(IQueryable<SizeGroup> groups, ListRequest request)
        {
            return groups.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
