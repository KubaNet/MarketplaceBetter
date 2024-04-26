using MarketplaceBetter.Domain.Entities.Sales.InputData;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class ReturnReasonModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ReturnReasonEnum SystemName { get; set; }
    }
}
