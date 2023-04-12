using AutoMapper;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Color;

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class ColorService : IColorService
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Color> _repository;
		private readonly IUserService _userService;

		public ColorService(
			IMapper mapper,
			IUnitOfWork unitOfWork,
			IUserService userService)
        {
			_mapper = mapper;
			_unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Color>();
            _userService = userService;
        }

        public ColorModel Get(long id) => _mapper.Map<ColorModel>(_repository.Get(id));

        public IList<ColorModel> GetAll() => _mapper.Map<IList<ColorModel>>(_repository.GetQuery().OrderBy(c => c.Name));

        public IList<ColorModel> GetAllForGroup(long groupId) => _mapper.Map<IList<ColorModel>>(_repository.GetQuery().Where(c => c.GroupId == groupId).OrderBy(c => c.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Color> colors = _repository.GetQuery();

            colors = ApplyFilter(colors, request);

            return colors.Count();
        }

        public IList<ColorModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Color> colors = _repository.GetQuery();

            colors = ApplyFilter(colors, request);
            colors = ApplySorting(colors, request);
            colors = ApplyPaging(colors, request);

            return _mapper.Map<IList<ColorModel>>(colors);
        }

        public void Add(ColorModel color)
        {
            Color colorToAdd = new();

            TransferValues(colorToAdd, color);

            _repository.Add(colorToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorModel color)
        {
            Color colorToUpdate = _repository.Get(color.Id);

            TransferValues(colorToUpdate, color);

            _repository.Update(colorToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Color toColor, ColorModel fromColor)
        {
            toColor.Name = fromColor.Name;
            toColor.Code = fromColor.Code;
            toColor.GroupId = fromColor.Group.Id;
        }

        private IQueryable<Color> ApplyFilter(IQueryable<Color> colors, ListRequest request)
        {
			if (_userService.IsSpecificBrand())
			{
				colors = colors.Where(c => c.Group.BrandId == _userService.GetCurrentBrand().Id);
			}

			if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return colors;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "code", "brand", "group" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    colors = searchField.Name switch
                    {
                        "id" => colors.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => colors.Where(c => c.Name.Contains(searchField.Value)),
                        "code" => colors.Where(c => c.Code.Contains(searchField.Value)),
                        "brand" => colors.Where(c => c.Group.Brand.Name.Contains(searchField.Value)),
                        "group" => colors.Where(c => c.Group.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    colors = colors.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Name.Contains(searchString)
                        || c.Code.Contains(searchString)
                        || c.Group.Brand.Name.Contains(searchString)
                        || c.Group.Name.Contains(searchString));
                }
            }

            return colors;
        }

        private IQueryable<Color> ApplySorting(IQueryable<Color> colors, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                colors = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? colors.OrderBy(c => c.Id) : colors.OrderByDescending(c => c.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? colors.OrderBy(c => c.Name) : colors.OrderByDescending(c => c.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? colors.OrderBy(c => c.Code) : colors.OrderByDescending(c => c.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? colors.OrderBy(c => c.Group.Brand.Name) : colors.OrderByDescending(c => c.Group.Brand.Name),
                    "group" => request.SortDirection == SortDirection.Ascending ? colors.OrderBy(c => c.Group.Name) : colors.OrderByDescending(c => c.Group.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return colors;
        }

        private IQueryable<Color> ApplyPaging(IQueryable<Color> colors, ListRequest request)
        {
            return colors.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
