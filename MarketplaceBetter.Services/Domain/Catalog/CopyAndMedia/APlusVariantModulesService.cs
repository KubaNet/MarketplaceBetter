using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia
{
    public class APlusVariantModulesService : IAPlusVariantModulesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<APlusModuleValue> _repository;
        private readonly IRepository<Instance> _instanceRepository;
        private readonly IUserService _userService;

        public APlusVariantModulesService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<APlusModuleValue>();
            _instanceRepository = unitOfWork.GetRepository<Instance>();
            _userService = userService;
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<APlusModuleValue> modules = _repository.GetQuery();

            modules = ApplyFilter(modules, request);

            var groupedModules = modules.GroupBy(m => new { m.ContentId, m.Content.InstanceId });

            return groupedModules.Count();
        }

        public IList<APlusVariantModulesModel> GetForListRequest(ListRequest request)
        {
            IQueryable<APlusModuleValue> modules = _repository.GetQuery();

            modules = ApplyFilter(modules, request);
            modules = ApplySorting(modules, request);

            var groupedPhotos = modules.ToList().GroupBy(m => new { m.ContentId, m.Content.InstanceId });

            if (!request.ShowAll)
            {
                groupedPhotos = groupedPhotos.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }

            return groupedPhotos.Select(m => new APlusVariantModulesModel
            {
                Content = _mapper.Map<APlusContentModel>(m.First().Content),
                Instance = _mapper.Map<InstanceModel>(m.First().Content.Instance),
                Modules = _mapper.Map<IList<APlusModuleValueModel>>(m.ToList()),
            }).ToList();
        }

        private IQueryable<APlusModuleValue> ApplyFilter(IQueryable<APlusModuleValue> modules, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                modules = modules.Where(m => m.Content.Product.BrandId == currentBrand.Id);
            }

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (currentInstance != null && _userService.IsSpecificInstance())
            {
                long instanceAllId = _instanceRepository.Single(i => i.SystemName == InstanceEnum.All).Id;
                modules = modules.Where(m => m.Content.InstanceId == _userService.GetCurrentInstance().Id || m.Content.InstanceId == instanceAllId);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return modules;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "instance" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    modules = searchField.Name switch
                    {
                        "instance" => modules.Where(m => m.Content.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    modules = modules.Where(m => m.Content.Instance.Name.Contains(searchString));
                }
            }

            return modules;
        }

        private IQueryable<APlusModuleValue> ApplySorting(IQueryable<APlusModuleValue> modules, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                modules = request.SortBy switch
                {
                    "instance" => request.SortDirection == SortDirection.Ascending ? modules.OrderBy(m => m.Content.Instance.Name) : modules.OrderByDescending(m => m.Content.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                modules = modules.OrderBy(m => m.Content.Name).ThenBy(m => m.Content.InstanceId);
            }

            return modules;
        }
    }
}
