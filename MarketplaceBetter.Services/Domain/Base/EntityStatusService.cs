using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base
{
    public class EntityStatusService : IEntityStatusService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<EntityStatus> _repository;

        public EntityStatusService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<EntityStatus>();
        }

        public IList<EntityStatusModel> GetAll() => _mapper.Map<IList<EntityStatusModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));
    }
}
