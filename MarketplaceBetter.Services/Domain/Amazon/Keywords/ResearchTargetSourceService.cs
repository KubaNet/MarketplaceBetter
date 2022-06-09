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
    public class ResearchTargetSourceService : IResearchTargetSourceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ResearchTargetSource> _repository;

        public ResearchTargetSourceService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<ResearchTargetSource>();
        }

        public IList<ResearchTargetSourceModel> GetAll() => _mapper.Map<IList<ResearchTargetSourceModel>>(_repository.GetQuery());
    }
}
