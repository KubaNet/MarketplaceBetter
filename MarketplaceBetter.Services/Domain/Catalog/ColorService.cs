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
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MarketplaceBetter.Domain.Entities.Catalog.Color> _repository;
        private readonly IMapper _mapper;

        public ColorService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<MarketplaceBetter.Domain.Entities.Catalog.Color>();
            _mapper = mapper;
        }

        public ColorModel Get(long id) => _mapper.Map<ColorModel>(_repository.Get(id));

        public IList<ColorModel> GetAll() => _mapper.Map<IList<ColorModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public IList<ColorModel> GetAllForGroup(long groupId) => _mapper.Map<IList<ColorModel>>(_repository.GetQuery().Where(c => c.GroupId == groupId));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> groups = _repository.GetQuery();

            ApplyFilter(groups, request);

            return groups.Count();
        }

        public IList<ColorModel> GetForListRequest(ListRequest request)
        {
            IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> colors = _repository.GetQuery();

            colors = ApplyFilter(colors, request);
            colors = ApplySorting(colors, request);
            colors = ApplyPaging(colors, request);

            return _mapper.Map<IList<ColorModel>>(colors);
        }

        public void Add(ColorModel color)
        {
            MarketplaceBetter.Domain.Entities.Catalog.Color colorToAdd = new();

            TransferValues(colorToAdd, color);

            _repository.Add(colorToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorModel color)
        {
            MarketplaceBetter.Domain.Entities.Catalog.Color colorToUpdate = _repository.Get(color.Id);

            TransferValues(colorToUpdate, color);

            _repository.Update(colorToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(MarketplaceBetter.Domain.Entities.Catalog.Color toColor, ColorModel fromColor)
        {
            toColor.Name = fromColor.Name;
            toColor.Code = fromColor.Code;
            toColor.GroupId = fromColor.Group.Id;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> ApplyFilter(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> colors, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return colors;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "code", "group", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    colors = searchField.Name switch
                    {
                        "id" => colors.Where(p => p.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => colors.Where(p => p.Name.Contains(searchField.Value)),
                        "code" => colors.Where(p => p.Code.Contains(searchField.Value)),
                        "group" => colors.Where(p => p.Group.Name.Contains(searchField.Value)),
                        "brand" => colors.Where(p => p.Group.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    colors = colors.Where(p => p.Id == searchString.ParseToIntOrDefault()
                        || p.Name.Contains(searchString)
                        || p.Code.Contains(searchString)
                        || p.Group.Name.Contains(searchString)
                        || p.Group.Brand.Name.Contains(searchString));
                }
            }

            return colors;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> ApplySorting(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> products, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Id) : products.OrderByDescending(p => p.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name),
                    "code" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Code) : products.OrderByDescending(p => p.Code),
                    "group" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Group.Name) : products.OrderByDescending(p => p.Group.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? products.OrderBy(p => p.Group.Brand.Name) : products.OrderByDescending(p => p.Group.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return products;
        }

        private IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> ApplyPaging(IQueryable<MarketplaceBetter.Domain.Entities.Catalog.Color> products, ListRequest request)
        {
            return products.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
