using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class AmazonEntityStatusService : IAmazonEntityStatusService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<AmazonEntityStatus> _repository;

        public AmazonEntityStatusService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<AmazonEntityStatus>();
        }

        public IList<AmazonEntityStatusModel> GetAll() => _mapper.Map<IList<AmazonEntityStatusModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));
    }
}
