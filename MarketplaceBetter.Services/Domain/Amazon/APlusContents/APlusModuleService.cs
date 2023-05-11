using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.APlusContents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.APlusContents
{
    public class APlusModuleService : IAPlusModuleService
    {
        private readonly IRepository<APlusModule> _repository;
        private readonly IMapper _mapper;

        public APlusModuleService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<APlusModule>();
            _mapper = mapper;
        }

        public IList<APlusModuleModel> GetAll() => _mapper.Map<IList<APlusModuleModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));
    }
}
