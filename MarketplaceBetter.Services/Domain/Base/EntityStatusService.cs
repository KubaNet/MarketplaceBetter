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
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Child> _childRepository;
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
            _parentRepository = unitOfWork.GetRepository<Parent>();
            _childRepository = unitOfWork.GetRepository<Child>();
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

        public void ChangeStatus(IList<ParentModel> parents, EntityStatusModel status)
        {
            foreach (var parent in parents)
            {
                Parent parentToUpdate = _parentRepository.Get(parent.Id);

                parentToUpdate.StatusId = status.Id;
                _parentRepository.Update(parentToUpdate);

                _unitOfWork.Save();

                //OnParentStatusChangeUp(parentToUpdate, status.Id);
                //OnParentStatusChangeOfChilds(parentToUpdate, status.Id);
            }
        }

        public void ChangeStatus(IList<ChildModel> childs, EntityStatusModel status)
        {
            foreach (var child in childs)
            {
                Child childToUpdate = _childRepository.Get(child.Id);
                childToUpdate.StatusId = status.Id;

                _childRepository.Update(childToUpdate);
                _unitOfWork.Save();

                //OnChildStatusChangeUp(childToUpdate, status.Id, true);
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

		private void OnProductStatusChangeDown(long productId, long statusId, bool updateVariants, bool updateParents)
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

            if (updateParents)
            {
                IList<Parent> parents = _parentRepository.Where(p => p.ProductId == productId).ToList();

                foreach (var parent in parents)
                {
                    parent.StatusId = statusId;
                    _parentRepository.Update(parent);

                    _unitOfWork.Save();
                }
            }
        }

        private void OnParentStatusChangeUp(Parent parent, long statusId)
        {
            if (!_parentRepository.Any(p => p.ProductId == parent.ProductId && p.StatusId != statusId))
            {
                Product product = _productRepository.Get(parent.ProductId);
                product.StatusId = statusId;

                _productRepository.Update(product);
                _unitOfWork.Save();

                OnProductStatusChangeDown(product.Id, statusId, true, false);
            }
        }

        private void OnParentStatusChangeOfChilds(Parent parent, long statusId)
        {
            IList<Child> childs = _childRepository.Where(c => c.Variant.ProductId == parent.ProductId && c.InstanceId == parent.InstanceId).ToList();
            foreach (var child in childs)
            {
                Child childToUpdate = _childRepository.Get(child.Id);
                childToUpdate.StatusId = statusId;

                _childRepository.Update(childToUpdate);
                _unitOfWork.Save();

                OnChildStatusChangeUp(childToUpdate, statusId, false);
            }
        }

        private void OnVariantStatusChangeDown(long variantId, long statusId)
        {
            IList<Child> childs = _childRepository.Where(c => c.VariantId == variantId).ToList();

            foreach (var child in childs)
            {
                child.StatusId = statusId;
                _childRepository.Update(child);
            }

            _unitOfWork.Save();
        }

        private void OnVariantStatusChangeUp(Variant variant, long statusId, bool updateParents)
        {
            if (!_variantRepository.Any(v => v.ProductId == variant.ProductId && v.StatusId != statusId))
            {
                Product product = _productRepository.Get(variant.ProductId);
                product.StatusId = statusId;

                _productRepository.Update(product);
                _unitOfWork.Save();

                OnProductStatusChangeDown(product.Id, statusId, false, updateParents);
            }
        }

        private void OnChildStatusChangeUp(Child child, long statusId, bool updateParents)
        {
            if (!_childRepository.Any(c => c.VariantId == child.VariantId && c.StatusId != statusId))
            {
                Variant variant = _variantRepository.Get(child.VariantId);
                variant.StatusId = statusId;

                _variantRepository.Update(variant);
                _unitOfWork.Save();

                OnVariantStatusChangeUp(variant, statusId, updateParents);
            }
        }
    }
}
