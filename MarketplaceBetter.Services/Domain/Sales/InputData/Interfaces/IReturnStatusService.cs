using MarketplaceBetter.Domain.Entities.Sales.InputData;

namespace MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces
{
    public interface IReturnStatusService
    {
        ReturnStatus GetByName(string name);
    }
}
