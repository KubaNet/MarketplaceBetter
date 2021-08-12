using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Size> _repository;
        private readonly IMapper _mapper;

        public SizeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Size>();
            _mapper = mapper;
        }

        public SizeModel Get(long id) => _mapper.Map<SizeModel>(_repository.Get(id));

        public IList<SizeModel> GetAll() => _mapper.Map<IList<SizeModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public IList<SizeModel> GetAllForGroup(long groupId) => _mapper.Map<IList<SizeModel>>(_repository.GetQuery().Where(s => s.GroupId == groupId));

        public void Add(SizeModel size)
        {
            Size sizeToAdd = new();

            TransferValues(sizeToAdd, size);

            _repository.Add(sizeToAdd);
            _unitOfWork.Save();
        }

        public void Update(SizeModel size)
        {
            Size sizeToUpdate = _repository.Get(size.Id);

            TransferValues(sizeToUpdate, size);

            _repository.Update(sizeToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Size toSize, SizeModel fromSize)
        {
            toSize.Name = fromSize.Name;
            toSize.Code = fromSize.Code;
            toSize.GroupId = fromSize.Group.Id;
        }
    }
}
