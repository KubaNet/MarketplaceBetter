using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class CurrentInstanceService : ICurrentInstanceService
    {
        private const string STORAGE_KEY = "CurrentInstance";
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepository<Instance> _instanceRepository;

        public CurrentInstanceService(
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _instanceRepository = unitOfWork.GetRepository<Instance>();
        }

        public InstanceModel GetCurrentInstance()
        {
            string instanceName = _configuration.GetValue<string>(STORAGE_KEY);

            return _mapper.Map<InstanceModel>(_instanceRepository.Single(b => b.Name == instanceName));
        }

        public void SetCurrentInstance(InstanceModel instance)
        {
            _configuration[STORAGE_KEY] = instance.Name;
        }

        public bool IsSpecificInstance()
        {
            InstanceModel instance = GetCurrentInstance();

            return instance != null && instance.SystemName != InstanceEnum.All;
        }
    }
}
