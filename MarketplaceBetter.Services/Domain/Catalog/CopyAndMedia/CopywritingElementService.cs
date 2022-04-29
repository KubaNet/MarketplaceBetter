using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Catalog.CopyAndMedia;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.CopyAndMedia
{
    public class CopywritingElementService : ICopywritingElementService
    {
        private readonly IRepository<CopywritingElement> _repository;
        private readonly IMapper _mapper;

        public CopywritingElementService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<CopywritingElement>();
            _mapper = mapper;
        }

        public IList<CopywritingElementModel> GetAll() => _mapper.Map<IList<CopywritingElementModel>>(_repository.GetQuery().OrderBy(e => e.Order));
    }
}
