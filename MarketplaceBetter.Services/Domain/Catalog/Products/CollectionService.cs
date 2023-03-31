using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class CollectionService : ICollectionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Collection> _repository;
        private readonly ICurrentBrandService _currentBrandService;

        public CollectionService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICurrentBrandService currentBrandService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Collection>();
            _currentBrandService = currentBrandService;
        }

        public CollectionModel Get(long id) => _mapper.Map<CollectionModel>(_repository.Get(id));

        public IList<CollectionModel> GetAll()
        {
            if (_currentBrandService.IsSpecificBrand())
            {
                return _mapper.Map<IList<CollectionModel>>(_repository.Where(c => c.BrandId == _currentBrandService.GetCurrentBrand().Id).OrderBy(g => g.Id));
            }
            else
            {
                return _mapper.Map<IList<CollectionModel>>(_repository.GetQuery().OrderBy(g => g.Id));
            }
        }

        public IList<CollectionModel> GetAllForBrand(long brandId) => _mapper.Map<IList<CollectionModel>>(_repository.GetQuery().Where(c => c.BrandId == brandId));

        public void Add(CollectionModel collection)
        {
            Collection collectionToAdd = new();

            TransferValues(collectionToAdd, collection);

            _repository.Add(collectionToAdd);
            _unitOfWork.Save();
        }

        public void Update(CollectionModel collection)
        {
            Collection collectionToUpdate = _repository.Get(collection.Id);

            TransferValues(collectionToUpdate, collection);

            _repository.Update(collectionToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Collection toCollection, CollectionModel fromCollection)
        {
            toCollection.Name = fromCollection.Name;
            toCollection.BrandId = fromCollection.Brand.Id;
        }
    }
}
