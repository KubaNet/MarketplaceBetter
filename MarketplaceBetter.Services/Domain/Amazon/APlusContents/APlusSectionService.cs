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
    public class APlusSectionService : IAPlusSectionService
    {
        private readonly IRepository<APlusSection> _repository;
        private readonly IMapper _mapper;

        public APlusSectionService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<APlusSection>();
            _mapper = mapper;
        }

        public IList<APlusSectionModel> GetAll() => _mapper.Map<IList<APlusSectionModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));
    }
}
