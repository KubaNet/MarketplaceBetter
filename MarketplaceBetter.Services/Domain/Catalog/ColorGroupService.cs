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
    public class ColorGroupService : IColorGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ColorGroup> _repository;
        private readonly IMapper _mapper;

        public ColorGroupService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ColorGroup>();
            _mapper = mapper;
        }

        public ColorGroupModel Get(long id) => _mapper.Map<ColorGroupModel>(_repository.Get(id));

        public IList<ColorGroupModel> GetAll() => _mapper.Map<IList<ColorGroupModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public void Add(ColorGroupModel group)
        {
            ColorGroup groupToAdd = new();

            TransferValues(groupToAdd, group);

            _repository.Add(groupToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorGroupModel group)
        {
            ColorGroup groupToUpdate = _repository.Get(group.Id);

            TransferValues(groupToUpdate, group);

            _repository.Update(groupToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ColorGroup toGroup, ColorGroupModel fromGroup)
        {
            toGroup.Name = fromGroup.Name;
        }
    }
}
