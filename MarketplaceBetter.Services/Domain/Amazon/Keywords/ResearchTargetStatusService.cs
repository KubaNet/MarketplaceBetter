using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Keywords
{
    public class ResearchTargetStatusService : IResearchTargetStatusService
    {
        private IRepository<ResearchTargetStatus> _repository;
        private readonly IMapper _mapper;

        public ResearchTargetStatusService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<ResearchTargetStatus>();
            _mapper = mapper;
        }

        public ResearchTargetStatusModel GetBySystemName(ResearchTargetStatusEnum systemName) => _mapper.Map<ResearchTargetStatusModel>(_repository.Single(s => s.SystemName == systemName));
    }
}
