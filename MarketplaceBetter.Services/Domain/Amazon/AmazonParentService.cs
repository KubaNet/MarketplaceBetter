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
    public class AmazonParentService : IAmazonParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AmazonParent> _repository;
        private readonly IMapper _mapper;

        public AmazonParentService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<AmazonParent>();
            _mapper = mapper;
        }

        public AmazonParentModel Get(long id)
        {
            AmazonParent parent = _repository.Get(id);

            return _mapper.Map<AmazonParentModel>(parent);
        }

        public IList<AmazonParentModel> GetAll()
        {
            IList<AmazonParentModel> parents = _mapper.Map<IList<AmazonParentModel>>(_repository.GetAll());

            return parents;
        }

        public IList<AmazonParentModel> GetAllForBrandAndInstance(long brandId, long instanceId)
        {
            IList<AmazonParentModel> parents = _mapper.Map<IList<AmazonParentModel>>(
                _repository.GetQuery().Where(p => p.Product.BrandId == brandId && p.InstanceId == instanceId));

            return parents;
        }

        public void Add(AmazonParentModel parent)
        {
            AmazonParent parentToAdd = new();

            TransferValues(parentToAdd, parent);

            _repository.Add(parentToAdd);
            _unitOfWork.Save();
        }

        public void Update(AmazonParentModel parent)
        {
            AmazonParent parentToUpdate = _repository.Get(parent.Id);

            TransferValues(parentToUpdate, parent);

            _repository.Update(parentToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(AmazonParent toAmazonParent, AmazonParentModel fromAmazonParent)
        {
            toAmazonParent.ProductId = fromAmazonParent.Product.Id;
            toAmazonParent.InstanceId = fromAmazonParent.Instance.Id;
            toAmazonParent.Sku = fromAmazonParent.Sku;
            toAmazonParent.Asin = fromAmazonParent.Asin;
        }
    }
}
