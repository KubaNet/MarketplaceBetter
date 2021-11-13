using MarketplaceBetter.Domain.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Interfaces
{
    public interface ICollectionService
    {
        CollectionModel Get(long id);

        IList<CollectionModel> GetAll();

        IList<CollectionModel> GetAllForBrand(long brandId);

        void Add(CollectionModel Collection);

        void Update(CollectionModel Collection);
    }
}
