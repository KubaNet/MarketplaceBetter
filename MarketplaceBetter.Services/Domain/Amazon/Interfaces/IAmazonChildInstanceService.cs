using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonChildInstanceService
    {
        AmazonChildInstanceModel Get(long id);

        IList<AmazonChildInstanceModel> GetAll();

        IList<AmazonChildInstanceModel> GetAllForBrand(long brandId, long instanceId);

        int CountForListRequest(ListRequest request);

        IList<AmazonChildInstanceModel> GetForListRequest(ListRequest request);

        void Add(AmazonChildInstanceModel Child);

        void Update(AmazonChildInstanceModel Child);
    }
}
