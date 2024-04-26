using MarketplaceBetter.Domain.Entities.Sales.InputData;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Sales.InputData.Interfaces;
using System;

namespace MarketplaceBetter.Services.Domain.Sales.InputData
{
    public class ReturnStatusService : IReturnStatusService
    {
        private readonly IRepository<ReturnStatus> _repository;

        public ReturnStatusService(
            IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<ReturnStatus>();
        }

        public ReturnStatus GetByName(string name) => _repository.Single(s => s.Name == name);
    }
}
