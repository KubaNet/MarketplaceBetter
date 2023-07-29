using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia
{
    public class PhotoCommentService : IPhotoCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Photo> _repository;

        public PhotoCommentService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Photo>();
        }

        public void Add(PhotoModel photo)
        {
            Update(photo);
        }

        public void Update(PhotoModel photo)
        {
            Photo photoTo = _repository.Get(photo.Id);

            photoTo.Comment = photo.Comment;

            _repository.Update(photoTo);
            _unitOfWork.Save();
        }

        public void Delete(PhotoModel photo)
        {
            photo.Comment = null;

            Update(photo);
        }
    }
}
