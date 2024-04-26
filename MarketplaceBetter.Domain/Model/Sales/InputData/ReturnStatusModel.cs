using MarketplaceBetter.Domain.Entities.Sales.InputData;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class ReturnStatusModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ReturnStatusEnum SystemName { get; set; }
    }
}
