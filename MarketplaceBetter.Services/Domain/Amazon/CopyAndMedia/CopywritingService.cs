using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia
{
    public class CopywritingService : ICopywritingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Copywriting> _repository;
        private readonly IUserService _userService;

        public CopywritingService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Copywriting>();
            _userService = userService;
        }

        public CopywritingModel Get(long id) => _mapper.Map<CopywritingModel>(_repository.Get(id));

        public CopywritingModel GetForProduct(long productId, long instanceId, CopywritingElementEnum element)
        {
            Copywriting copywriting = _repository.SingleOrDefault(c => c.ProductId == productId & c.InstanceId == instanceId & c.Element.SystemName == element);

            return _mapper.Map<CopywritingModel>(copywriting);
        }

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<Copywriting> copywritings = _repository.GetQuery();

            copywritings = ApplyFilter(copywritings, request);

            return copywritings.Count();
        }

        public IList<CopywritingModel> GetForListRequest(ListRequest request)
        {
            IQueryable<Copywriting> copywritings = _repository.GetQuery();

            copywritings = ApplyFilter(copywritings, request);
            copywritings = ApplySorting(copywritings, request);
            copywritings = ApplyPaging(copywritings, request);

            return _mapper.Map<IList<CopywritingModel>>(copywritings);
        }

        public void Add(CopywritingModel copywriting)
        {
            Copywriting copywritingToAdd = new();

            TransferValues(copywritingToAdd, copywriting);

            _repository.Add(copywritingToAdd);
            _unitOfWork.Save();
        }

        public void Update(CopywritingModel copywriting)
        {
            Copywriting copywritingToUpdate = _repository.Get(copywriting.Id);

            TransferValues(copywritingToUpdate, copywriting);

            _repository.Update(copywritingToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Copywriting toCopywriting, CopywritingModel fromCopywriting)
        {
            toCopywriting.ProductId = fromCopywriting.Product.Id;
            toCopywriting.InstanceId = fromCopywriting.Instance.Id;
            toCopywriting.ElementId = fromCopywriting.Element.Id;
            toCopywriting.Value = fromCopywriting.Value;
        }

        private IQueryable<Copywriting> ApplyFilter(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            BrandModel currentBrand = _userService.GetCurrentBrand();
            if (_userService.IsSpecificBrand())
            {
                copywritings = copywritings.Where(c => c.Product.BrandId == currentBrand.Id);
            }

            CollectionModel currentCollection = _userService.GetCurrentCollection();
            if (_userService.IsSpecificCollection())
            {
                copywritings = copywritings.Where(c => c.Product.CollectionId == currentCollection.Id);
            }

            InstanceModel currentInstance = _userService.GetCurrentInstance();
            if (_userService.IsSpecificInstance())
            {
                copywritings = copywritings.Where(c => c.InstanceId == currentInstance.Id);
            }

            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return copywritings;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "product", "instance", "element", "value" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    copywritings = searchField.Name switch
                    {
                        "id" => copywritings.Where(c => c.Id == searchField.Value.ParseToIntOrDefault()),
                        "product" => copywritings.Where(c => c.Product.Code.Contains(searchField.Value)),
                        "instance" => copywritings.Where(c => c.Instance.Name.Contains(searchField.Value)),
                        "element" => copywritings.Where(c => c.Element.Name.Contains(searchField.Value)),
                        "value" => copywritings.Where(c => c.Value.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    copywritings = copywritings.Where(c => c.Id == searchString.ParseToIntOrDefault()
                        || c.Product.Code.Contains(searchString)
                        || c.Product.Name.Contains(searchString)
                        || c.Instance.Name.Contains(searchString)
                        || c.Element.Name.Contains(searchString)
                        || c.Value.Contains(searchString));
                }
            }

            return copywritings;
        }

        private IQueryable<Copywriting> ApplySorting(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                copywritings = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Id) : copywritings.OrderByDescending(c => c.Id),
                    "product" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Product.Code) : copywritings.OrderByDescending(c => c.Product.Code),
                    "instance" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Instance.Name) : copywritings.OrderByDescending(c => c.Instance.Name),
                    "element" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Element.Name) : copywritings.OrderByDescending(c => c.Element.Name),
                    "value" => request.SortDirection == SortDirection.Ascending ? copywritings.OrderBy(c => c.Value) : copywritings.OrderByDescending(c => c.Value),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }
            else
            {
                copywritings = copywritings.OrderBy(c => c.Id);
            }

            return copywritings;
        }

        private IQueryable<Copywriting> ApplyPaging(IQueryable<Copywriting> copywritings, ListRequest request)
        {
            return copywritings.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
