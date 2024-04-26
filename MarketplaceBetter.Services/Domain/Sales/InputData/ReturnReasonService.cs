using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces;

namespace MarketplaceBetter.Services.Domain.Sales.InputData
{
    public class ReturnReasonService : IReturnReasonService
    {
        private readonly IRepository<ReturnReason> _repository;

        public ReturnReasonService(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ReturnReason>();
        }
        public ReturnReason GetByName(string name) => _repository.Single(r => r.Name == name);
    }
}
