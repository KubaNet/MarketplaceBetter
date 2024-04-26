using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces;

namespace MarketplaceBetter.Services.Domain.Sales.InputData
{
    public class ReturnDetailedDispositionService : IReturnDetailedDispositionService
    {
        private readonly IRepository<ReturnDetailedDisposition> _repository;

        public ReturnDetailedDispositionService(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ReturnDetailedDisposition>();
        }
        public ReturnDetailedDisposition GetByName(string name) => _repository.Single(d => d.Name == name);
    }
}
