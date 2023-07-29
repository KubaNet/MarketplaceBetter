using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class BrandCollectionService : IBrandCollectionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Collection> _collectionRepository;

        public BrandCollectionService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _brandRepository = unitOfWork.GetRepository<Brand>();
            _collectionRepository = unitOfWork.GetRepository<Collection>();
        }

        public IList<BrandCollectionModel> GetAll()
        {
            IList<BrandCollectionModel> brandsAndCollections = new List<BrandCollectionModel>();

            foreach (var brand in _brandRepository.GetAll().OrderBy(b => b.Name))
            {
                brandsAndCollections.Add(new BrandCollectionModel { Name = brand.Name, Brand = _mapper.Map<BrandModel>(brand), IsBrand = true });

                foreach (var collection in _collectionRepository.Where(c => c.BrandId == brand.Id).OrderBy(c => c.Name))
                {
                    brandsAndCollections.Add(new BrandCollectionModel { Name = $"-- {collection.Name}", Collection = _mapper.Map<CollectionModel>(collection), IsCollection = true });
                }
            }

            return brandsAndCollections;
        }
    }
}
