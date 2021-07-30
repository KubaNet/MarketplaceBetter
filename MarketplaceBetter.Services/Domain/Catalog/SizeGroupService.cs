using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Catalog
{
    public class SizeGroupService : ISizeGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SizeGroup> _repository;
        private readonly IMapper _mapper;

        public SizeGroupService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<SizeGroup>();
            _mapper = mapper;
        }

        public SizeGroupModel Get(long id)
        {
            SizeGroup group = _repository.Get(id);

            return _mapper.Map<SizeGroupModel>(group);
        }

        public IList<SizeGroupModel> GetAll()
        {
            IList<SizeGroupModel> groups = _mapper.Map<IList<SizeGroupModel>>(_repository.GetAll());

            return groups;
        }

        public void Add(SizeGroupModel group)
        {
            SizeGroup groupToAdd = new();

            TransferValues(groupToAdd, group);

            _repository.Add(groupToAdd);
            _unitOfWork.Save();
        }

        public void Update(SizeGroupModel group)
        {
            SizeGroup groupToUpdate = _repository.Get(group.Id);

            TransferValues(groupToUpdate, group);

            _repository.Update(groupToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(SizeGroup toGroup, SizeGroupModel gromGroup)
        {
            toGroup.Name = gromGroup.Name;
        }
    }
}
