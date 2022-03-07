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
    public class AmazonTargetingTypeService : IAmazonTargetingTypeService
    {
        private IRepository<AmazonTargetingType> _repository;
        private readonly IMapper _mapper;

        public AmazonTargetingTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<AmazonTargetingType>();
            _mapper = mapper;
        }

        public AmazonTargetingTypeModel GetBySystemName(AmazonTargetingTypeEnum systemName) => _mapper.Map<AmazonTargetingTypeModel>(_repository.Single(s => s.SystemName == systemName));
    }
}
