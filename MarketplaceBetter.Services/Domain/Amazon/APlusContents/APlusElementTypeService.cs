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
    public class APlusElementTypeService : IAPlusElementTypeService
    {
        private readonly IRepository<APlusElementType> _repository;
        private readonly IMapper _mapper;

        public APlusElementTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<APlusElementType>();
            _mapper = mapper;
        }

        public IList<APlusElementTypeModel> GetAll() => _mapper.Map<IList<APlusElementTypeModel>>(_repository.GetQuery().OrderBy(e => e.SystemName));
    }
}
