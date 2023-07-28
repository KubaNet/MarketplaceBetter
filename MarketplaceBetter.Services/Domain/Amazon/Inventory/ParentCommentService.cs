using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class ParentCommentService : IParentCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Parent> _repository;

        public ParentCommentService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Parent>();
        }

        public void Add(ParentModel parent)
        {
            Update(parent);
        }

        public void Update(ParentModel parent)
        {
            Parent parentTo = _repository.Get(parent.Id);

            parentTo.Comment = parent.Comment;

            _repository.Update(parentTo);
            _unitOfWork.Save();
        }

        public void Delete(ParentModel parent)
        {
            parent.Comment = null;

            Update(parent);
        }
    }
}
