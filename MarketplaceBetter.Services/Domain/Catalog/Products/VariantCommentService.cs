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
    public class VariantCommentService : IVariantCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Variant> _repository;

        public VariantCommentService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Variant>();
        }

        public void Add(VariantModel variant)
        {
            Update(variant);
        }

        public void Update(VariantModel variant)
        {
            Variant variantTo = _repository.Get(variant.Id);

            variantTo.Comment = variant.Comment;

            _repository.Update(variantTo);
            _unitOfWork.Save();
        }

        public void Delete(VariantModel variant)
        {
            variant.Comment = null;

            Update(variant);
        }
    }
}
