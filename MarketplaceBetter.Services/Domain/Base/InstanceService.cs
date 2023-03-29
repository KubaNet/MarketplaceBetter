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

        public IList<InstanceModel> GetAll() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().OrderBy(i => i.Id));

        public IList<InstanceModel> GetAllNormal() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().Where(i => i.IsNormal).OrderBy(i => i.Id));
    }
}
