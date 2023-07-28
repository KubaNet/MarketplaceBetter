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
    public class ChildCommentService : IChildCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Child> _repository;

        public ChildCommentService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Child>();
        }

        public void Add(ChildModel child)
        {
            Update(child);
        }

        public void Update(ChildModel child)
        {
            Child childTo = _repository.Get(child.Id);

            childTo.Comment = child.Comment;

            _repository.Update(childTo);
            _unitOfWork.Save();
        }

        public void Delete(ChildModel child)
        {
            child.Comment = null;

            Update(child);
        }
    }
}
