using AutoMapper;
using MarketplaceBetter.Domain.Entities.Sales;
using MarketplaceBetter.Domain.Model.Sales;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Sales
{
    public class InstanceService : IInstanceService
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public InstanceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<Instance>();
            _mapper = mapper;
        }

        public IList<InstanceModel> GetAll() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().OrderBy(i => i.Order));

        public IList<InstanceModel> GetAllForAmazon() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().Where(i => i.SalesChannel.SystemName == SalesChannelEnum.Amazon).OrderBy(i => i.Order));
    }
}
