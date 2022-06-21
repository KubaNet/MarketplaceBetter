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
    public class CampaignStrategyService : ICampaignStrategyService
    {
        private readonly IRepository<CampaignStrategy> _repository;
        private readonly IMapper _mapper;

        public CampaignStrategyService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<CampaignStrategy>();
            _mapper = mapper;
        }

        public IList<CampaignStrategyModel> GetAllByCampaignType(CampaignTypeEnum campaignType) => 
            _mapper.Map<IList<CampaignStrategyModel>>(_repository.Where(s => s.CampaignType.SystemName == campaignType).OrderBy(s => s.SystemName));
    }
}
