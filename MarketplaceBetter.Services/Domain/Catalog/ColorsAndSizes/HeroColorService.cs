using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
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

namespace MarketplaceBetter.Services.Domain.Catalog.ColorsAndSizes
{
    public class HeroColorService : IHeroColorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<HeroColor> _repository;
        private readonly IUserService _userService;

        public HeroColorService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<HeroColor>();
            _userService = userService;
        }

        public HeroColorModel Get(long id) => _mapper.Map<HeroColorModel>(_repository.Get(id));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<HeroColor> heroColors = _repository.GetQuery();

            heroColors = ApplyFilter(heroColors, request);

            return heroColors.Count();
        }

        public IList<HeroColorModel> GetForListRequest(ListRequest request)
        {
            IQueryable<HeroColor> heroColors = _repository.GetQuery();

            heroColors = ApplyFilter(heroColors, request);
            heroColors = ApplySorting(heroColors, request);
            heroColors = ApplyPaging(heroColors, request);

            return _mapper.Map<IList<HeroColorModel>>(heroColors);
        }

        public void Add(HeroColorModel heroColor)
        {
            HeroColor heroColorToAdd = new();

            TransferValues(heroColorToAdd, heroColor);

            _repository.Add(heroColorToAdd);
            _unitOfWork.Save();
        }

        public void Update(HeroColorModel heroColor)
        {
            HeroColor heroColorToUpdate = _repository.Get(heroColor.Id);

            TransferValues(heroColorToUpdate, heroColor);

            _repository.Update(heroColorToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(HeroColor toHeroColor, HeroColorModel fromHeroColor)
        {
            toHeroColor.ProductId = fromHeroColor.Product.Id;
            toHeroColor.ColorId = fromHeroColor.Color.Id;
        }

        private IQueryable<HeroColor> ApplyFilter(IQueryable<HeroColor> heroColors, ListRequest request)
        {
            if (_userService.IsSpecificBrand())
            {
                BrandModel currentBrand = _userService.GetCurrentBrand();
                heroColors = heroColors.Where(c => c.Product.Brand.Id == currentBrand.Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return heroColors;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "product_code", "color" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    heroColors = searchField.Name switch
                    {
                        "id" => heroColors.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => heroColors.Where(c => c.Product.Name.Contains(searchField.Value)),
                        "product_code" => heroColors.Where(c => c.Product.Code.Contains(searchField.Value)),
                        "color" => heroColors.Where(c => c.Color.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    heroColors = heroColors.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Product.Name.Contains(searchString)
                        || c.Product.Code.Contains(searchString)
                        || c.Color.Name.Contains(searchString));
                }
            }

            return heroColors;
        }

        private IQueryable<HeroColor> ApplySorting(IQueryable<HeroColor> heroColors, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                heroColors = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? heroColors.OrderBy(c => c.Id) : heroColors.OrderByDescending(c => c.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? heroColors.OrderBy(c => c.Product.Name) : heroColors.OrderByDescending(c => c.Product.Name),
                    "product_code" => request.SortDirection == SortDirection.Ascending ? heroColors.OrderBy(c => c.Product.Code) : heroColors.OrderByDescending(c => c.Product.Code),
                    "color" => request.SortDirection == SortDirection.Ascending ? heroColors.OrderBy(c => c.Color.Name) : heroColors.OrderByDescending(c => c.Color.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return heroColors;
        }

        private IQueryable<HeroColor> ApplyPaging(IQueryable<HeroColor> heroColors, ListRequest request)
        {
            return heroColors.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
