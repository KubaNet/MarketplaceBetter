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
    public class BiddingStrategyService : IBiddingStrategyService
    {
        private readonly IRepository<BiddingStrategy> _repository;
        private readonly IMapper _mapper;

        public BiddingStrategyService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<BiddingStrategy>();
            _mapper = mapper;
        }

        public IList<BiddingStrategyModel> GetAll() => _mapper.Map<IList<BiddingStrategyModel>>(_repository.GetQuery().OrderBy(t => t.SystemName));
    }
}
