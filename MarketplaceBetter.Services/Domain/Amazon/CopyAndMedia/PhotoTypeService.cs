using AutoMapper;
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
    public class PhotoTypeService : IPhotoTypeService
    {
        private readonly IRepository<PhotoType> _repository;
        private readonly IMapper _mapper;

        public PhotoTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<PhotoType>();
            _mapper = mapper;
        }

        public IList<PhotoTypeModel> GetAll() => _mapper.Map<IList<PhotoTypeModel>>(_repository.GetQuery().OrderBy(e => e.SystemName));
    }
}
