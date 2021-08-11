using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class AmazonChildService : IAmazonChildService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonChild> _repository;
        private readonly IMapper _mapper;

        public AmazonChildService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonChild>();
            _mapper = mapper;
        }

        public AmazonChildModel Get(long id)
        {
            AmazonChild child = _repository.Get(id);

            return _mapper.Map<AmazonChildModel>(child);
        }

        public IList<AmazonChildModel> GetAll()
        {
            IList<AmazonChildModel> children = _mapper.Map<IList<AmazonChildModel>>(_repository.GetAll().OrderBy(g => g.Sku));

            return children;
        }

        public void Add(AmazonChildModel child)
        {
            AmazonChild childToAdd = new();

            TransferValues(childToAdd, child);

            _repository.Add(childToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonChildModel child)
        {
            AmazonChild childToUpdate = _repository.Get(child.Id);

            TransferValues(childToUpdate, child);

            _repository.Update(childToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonChild toChild, AmazonChildModel fromChild)
        {
            toChild.ParentId = fromChild.Parent.Id;
            toChild.ProductVariantId = fromChild.ProductVariant.Id;
            toChild.Sku = fromChild.Sku;
            toChild.Asin = fromChild.Asin;
        }
    }
}
