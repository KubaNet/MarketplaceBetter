using MarketplaceBetter.Domain.Entities.Sales.InputData;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class ReturnDetailedDispositionModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ReturnDetailedDispositionEnum SystemName { get; set; }
    }
}
