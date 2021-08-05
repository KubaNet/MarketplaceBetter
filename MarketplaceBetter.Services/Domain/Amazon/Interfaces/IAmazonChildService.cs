using MarketplaceBetter.Domain.Model.Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Interfaces
{
    public interface IAmazonChildService
    {
        AmazonChildModel Get(long id);

        IList<AmazonChildModel> GetAll();

        void Add(AmazonChildModel child);

        void Update(AmazonChildModel child);
    }
}
