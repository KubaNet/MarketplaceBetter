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
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;

        public ProductCommentService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Product>();
        }

        public void Add(ProductModel product)
        {
            Update(product);
        }

        public void Update(ProductModel product)
        {
            Product productTo = _repository.Get(product.Id);

            productTo.Comment = product.Comment;

            _repository.Update(productTo);
            _unitOfWork.Save();
        }

        public void Delete(ProductModel product)
        {
            product.Comment = null;

            Update(product);
        }
    }
}
