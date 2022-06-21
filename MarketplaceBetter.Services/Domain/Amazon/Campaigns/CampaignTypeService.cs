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
    public class CampaignTypeService : ICampaignTypeService
    {
        private readonly IRepository<CampaignType> _repository;
        private readonly IMapper _mapper;

        public CampaignTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<CampaignType>();
            _mapper = mapper;
        }

        public IList<CampaignTypeModel> GetAll() => _mapper.Map<IList<CampaignTypeModel>>(_repository.GetQuery().OrderBy(t => t.SystemName));
    }
}
