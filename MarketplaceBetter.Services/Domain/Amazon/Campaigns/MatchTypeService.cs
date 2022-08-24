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
    public class MatchTypeService : IMatchTypeService
    {
        private readonly IRepository<MatchType> _repository;
        private readonly IMapper _mapper;

        public MatchTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<MatchType>();
            _mapper = mapper;
        }

        public IList<MatchTypeModel> GetAll() => _mapper.Map<IList<MatchTypeModel>>(_repository.GetQuery().OrderBy(t => t.SystemName));

        public IList<MatchTypeModel> GetAllNegative() => _mapper.Map<IList<MatchTypeModel>>(_repository.GetQuery().Where(t => t.SystemName == MatchTypeEnum.NegativeExact || t.SystemName == MatchTypeEnum.NegativePhrase).OrderBy(t => t.SystemName));
    }
}
