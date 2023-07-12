using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base
{
    public class EntityStatusService : IEntityStatusService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<EntityStatus> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Variant> _variantRepository;
        private readonly IRepository<ParentInstance> _parentRepository;
        private readonly IRepository<ChildInstance> _childRepository;
        private readonly IRepository<APlusContent> _contentRepository;

        public EntityStatusService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<EntityStatus>();
            _productRepository = unitOfWork.GetRepository<Product>();
            _variantRepository = unitOfWork.GetRepository<Variant>();
            _parentRepository = unitOfWork.GetRepository<ParentInstance>();
            _childRepository = unitOfWork.GetRepository<ChildInstance>();
            _contentRepository = unitOfWork.GetRepository<APlusContent>();
        }

        public IList<EntityStatusModel> GetAll() => _mapper.Map<IList<EntityStatusModel>>(_repository.GetQuery().OrderBy(s => s.SystemName));

        public void ChangeStatus(IList<ProductModel> products, EntityStatusModel status)
        {
            foreach (var product in products)
            {
                Product productToUpdate = _productRepository.Get(product.Id);

                productToUpdate.StatusId = status.Id;
                _productRepository.Update(productToUpdate);

                _unitOfWork.Save();

                //OnProductStatusChangeDown(product.Id, status.Id, true, true);
            }
        }

        public void ChangeStatus(IList<VariantModel> variants, EntityStatusModel status)
        {
            foreach (var variant in variants)
            {
                Variant variantToUpdate = _variantRepository.Get(variant.Id);

                variantToUpdate.StatusId = status.Id;
                _variantRepository.Update(variantToUpdate);

                _unitOfWork.Save();

                //OnVariantStatusChangeDown(variant.Id, status.Id);
                //OnVariantStatusChangeUp(variantToUpdate, status.Id, true);
            }
        }

        public void ChangeStatus(IList<ParentInstanceModel> parents, EntityStatusModel status)
        {
            foreach (var parent in parents)
            {
                ParentInstance parentToUpdate = _parentRepository.Get(parent.Id);

                parentToUpdate.StatusId = status.Id;
                _parentRepository.Update(parentToUpdate);

                _unitOfWork.Save();

                //OnParentInstanceStatusChangeUp(parentToUpdate, status.Id);
                //OnParentInstanceStatusChangeOfChilds(parentToUpdate, status.Id);
            }
        }

        public void ChangeStatus(IList<ChildInstanceModel> childs, EntityStatusModel status)
        {
            foreach (var child in childs)
            {
                ChildInstance childToUpdate = _childRepository.Get(child.Id);
                childToUpdate.StatusId = status.Id;

                _childRepository.Update(childToUpdate);
                _unitOfWork.Save();

                //OnChildInstanceStatusChangeUp(childToUpdate, status.Id, true);
            }
        }

		public void ChangeStatus(IList<APlusContentModel> contents, EntityStatusModel status)
		{
			foreach (var content in contents)
			{
				APlusContent contentToUpdate = _contentRepository.Get(content.Id);
				contentToUpdate.StatusId = status.Id;

				_contentRepository.Update(contentToUpdate);
                _unitOfWork.Save();
			}
		}

		private void OnProductStatusChangeDown(long productId, long statusId, bool updateVariants, bool updateParentInstances)
        {
            if (updateVariants)
            {
                IList<Variant> variants = _variantRepository.Where(v => v.ProductId == productId).ToList();

                foreach (var variant in variants)
                {
                    variant.StatusId = statusId;
                    _variantRepository.Update(variant);

                    _unitOfWork.Save();

                    OnVariantStatusChangeDown(variant.Id, statusId);
                }
            }

            if (updateParentInstances)
            {
                IList<ParentInstance> parentInstances = _parentRepository.Where(p => p.ProductId == productId).ToList();

                foreach (var parentInstance in parentInstances)
                {
                    parentInstance.StatusId = statusId;
                    _parentRepository.Update(parentInstance);

                    _unitOfWork.Save();
                }
            }
        }

        private void OnParentInstanceStatusChangeUp(ParentInstance parentInstance, long statusId)
        {
            if (!_parentRepository.Any(p => p.ProductId == parentInstance.ProductId && p.StatusId != statusId))
            {
                Product product = _productRepository.Get(parentInstance.ProductId);
                product.StatusId = statusId;

                _productRepository.Update(product);
                _unitOfWork.Save();

                OnProductStatusChangeDown(product.Id, statusId, true, false);
            }
        }

        private void OnParentInstanceStatusChangeOfChilds(ParentInstance parentInstance, long statusId)
        {
            IList<ChildInstance> childInstances = _childRepository.Where(c => c.Variant.ProductId == parentInstance.ProductId && c.InstanceId == parentInstance.InstanceId).ToList();
            foreach (var childInstance in childInstances)
            {
                ChildInstance childInstanceToUpdate = _childRepository.Get(childInstance.Id);
                childInstanceToUpdate.StatusId = statusId;

                _childRepository.Update(childInstanceToUpdate);
                _unitOfWork.Save();

                OnChildInstanceStatusChangeUp(childInstanceToUpdate, statusId, false);
            }
        }

        private void OnVariantStatusChangeDown(long variantId, long statusId)
        {
            IList<ChildInstance> childInstances = _childRepository.Where(c => c.VariantId == variantId).ToList();

            foreach (var childInstance in childInstances)
            {
                childInstance.StatusId = statusId;
                _childRepository.Update(childInstance);
            }

            _unitOfWork.Save();
        }

        private void OnVariantStatusChangeUp(Variant variant, long statusId, bool updateParentInstances)
        {
            if (!_variantRepository.Any(v => v.ProductId == variant.ProductId && v.StatusId != statusId))
            {
                Product product = _productRepository.Get(variant.ProductId);
                product.StatusId = statusId;

                _productRepository.Update(product);
                _unitOfWork.Save();

                OnProductStatusChangeDown(product.Id, statusId, false, updateParentInstances);
            }
        }

        private void OnChildInstanceStatusChangeUp(ChildInstance childInstance, long statusId, bool updateParentInstances)
        {
            if (!_childRepository.Any(c => c.VariantId == childInstance.VariantId && c.StatusId != statusId))
            {
                Variant variant = _variantRepository.Get(childInstance.VariantId);
                variant.StatusId = statusId;

                _variantRepository.Update(variant);
                _unitOfWork.Save();

                OnVariantStatusChangeUp(variant, statusId, updateParentInstances);
            }
        }
    }
}
