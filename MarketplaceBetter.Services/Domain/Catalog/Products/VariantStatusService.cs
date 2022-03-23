using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog.Products
{
    public class VariantStatusService : IVariantStatusService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<VariantStatus> _repository;

        public VariantStatusService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork.GetRepository<VariantStatus>();
        }

        public IList<VariantStatusModel> GetAll() => _mapper.Map<IList<VariantStatusModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));
    }
}
