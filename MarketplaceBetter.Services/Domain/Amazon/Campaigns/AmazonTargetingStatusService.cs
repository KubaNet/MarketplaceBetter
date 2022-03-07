using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Model.Amazon.Campaigns;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Campaigns.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Campaigns
{
    public class AmazonTargetingStatusService : IAmazonTargetingStatusService
    {
        private IRepository<AmazonTargetingStatus> _repository;
        private readonly IMapper _mapper;

        public AmazonTargetingStatusService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<AmazonTargetingStatus>();
            _mapper = mapper;
        }

        public AmazonTargetingStatusModel GetBySystemName(AmazonTargetingStatusEnum systemName) => _mapper.Map<AmazonTargetingStatusModel>(_repository.Single(s => s.SystemName == systemName));
    }
}
