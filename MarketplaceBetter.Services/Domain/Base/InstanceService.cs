using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Specialized.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base
{
    public class InstanceService : IInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Instance> _repository;
        private readonly ICurrentInstanceService _currentInstanceService;

        public InstanceService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICurrentInstanceService currentInstanceService)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<Instance>();
            _currentInstanceService = currentInstanceService;
        }

        public IList<InstanceModel> GetAll() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().OrderBy(i => i.Id));

        public IList<InstanceModel> GetAllNormal() => _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().Where(i => i.IsNormal).OrderBy(i => i.Id));

        public IList<InstanceModel> GetAllNormal(bool onlyCurrent)
        {
            if (onlyCurrent && _currentInstanceService.IsSpecificInstance())
            {
                return _mapper.Map<IList<InstanceModel>>(_repository.Where(i => i.Id == _currentInstanceService.GetCurrentInstance().Id));
            }
            else
            {
                return _mapper.Map<IList<InstanceModel>>(_repository.GetQuery().Where(i => i.IsNormal).OrderBy(i => i.Id));
            }
        }
    }
}
