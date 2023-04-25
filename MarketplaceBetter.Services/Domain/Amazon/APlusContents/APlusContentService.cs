using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
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
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
    public class APlusContentService : IAPlusContentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<APlusContent> _repository;
        private readonly IUserService _userService;

        public APlusContentService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<APlusContent>();
            _userService = userService;
        }

        public APlusContentModel Get(long id) => _mapper.Map<APlusContentModel>(_repository.Get(id));

        public IList<APlusContentModel> GetAll(bool onlyCurrent)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (onlyCurrent && _userService.IsSpecificBrand() && currentBrand != null)
            {
                return _mapper.Map<IList<APlusContentModel>>(_repository.Where(c => c.Product.Brand.Id == currentBrand.Id).OrderBy(c => c.Name));
            }
            else
            {
                return _mapper.Map<IList<APlusContentModel>>(_repository.GetQuery().OrderBy(c => c.Name));
            }
        }

        public IList<APlusContentModel> GetAllFor(BrandModel brand, ProductModel product, InstanceModel instance)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                contents = contents.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            if (brand != null)
            {
                contents = contents.Where(c => c.Product.BrandId == brand.Id);
            }

            if (product  != null)
            {
                contents = contents.Where(c => c.ProductId == product.Id);
            }

            if (instance != null)
            {
                contents = contents.Where(c => c.InstanceId == instance.Id);
            }

            return _mapper.Map<IList<APlusContentModel>>(contents.OrderBy(c => c.Name));
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            contents = ApplyFilter(contents, request);

            return contents.Count();
        }

        public IList<APlusContentModel> GetForListRequest(ListRequest request)
        {
            IQueryable<APlusContent> contents = _repository.GetQuery();

            contents = ApplyFilter(contents, request);
            contents = ApplySorting(contents, request);
            contents = ApplyPaging(contents, request);

            return _mapper.Map<IList<APlusContentModel>>(contents);
        }

        public void Add(APlusContentModel content)
        {
            APlusContent contentToAdd = new();

            TransferValues(contentToAdd, content);

            _repository.Add(contentToAdd);
            _unitOfWork.Save();
        }

        public void Update(APlusContentModel content)
        {
            APlusContent contentToUpdate = _repository.Get(content.Id);

            TransferValues(contentToUpdate, content);

            _repository.Update(contentToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(APlusContent toContent, APlusContentModel fromContent)
        {
            toContent.Name = fromContent.Name;
            toContent.ProductId = fromContent.Product.Id;
            toContent.InstanceId = fromContent.Instance.Id;
        }

        private IQueryable<APlusContent> ApplyFilter(IQueryable<APlusContent> contents, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (currentBrand != null && _userService.IsSpecificBrand())
            {
                contents = contents.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return contents;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "product", "brand", "instance" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    contents = searchField.Name switch
                    {
                        "id" => contents.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => contents.Where(c => c.Name.Contains(searchField.Value)),
                        "product" => contents.Where(c => c.Product.Code.Contains(searchField.Value)),
                        "brand" => contents.Where(c => c.Product.Brand.Name.Contains(searchField.Value)),
                        "instance" => contents.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    contents = contents.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Name.Contains(searchString)
                        || c.Product.Code.Contains(searchString)
                        || c.Product.Brand.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString));
                }
            }

            return contents;
        }

        private IQueryable<APlusContent> ApplySorting(IQueryable<APlusContent> contents, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                contents = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Id) : contents.OrderByDescending(c => c.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Name) : contents.OrderByDescending(c => c.Name),
                    "product" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Product.Code) : contents.OrderByDescending(c => c.Product.Code),
                    "brand" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Product.Brand.Name) : contents.OrderByDescending(c => c.Product.Brand.Name),
                    "instance" => request.SortDirection == SortDirection.Ascending ? contents.OrderBy(c => c.Instance.Name) : contents.OrderByDescending(c => c.Instance.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                contents = contents.OrderBy(c => c.Name);
            }

            return contents;
        }

        private IQueryable<APlusContent> ApplyPaging(IQueryable<APlusContent> contents, ListRequest request)
        {
            return contents.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
