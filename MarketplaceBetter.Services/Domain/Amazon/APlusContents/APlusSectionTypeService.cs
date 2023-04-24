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
    public class APlusSectionTypeService : IAPlusSectionTypeService
    {
        private readonly IRepository<APlusSectionType> _repository;
        private readonly IMapper _mapper;

        public APlusSectionTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<APlusSectionType>();
            _mapper = mapper;
        }

        public IList<APlusSectionTypeModel> GetAll() => _mapper.Map<IList<APlusSectionTypeModel>>(_repository.GetQuery().OrderBy(e => e.SystemName));
    }
}
