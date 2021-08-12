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
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Color> _repository;
        private readonly IMapper _mapper;

        public ColorService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Color>();
            _mapper = mapper;
        }

        public ColorModel Get(long id) => _mapper.Map<ColorModel>(_repository.Get(id));

        public IList<ColorModel> GetAll() => _mapper.Map<IList<ColorModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public IList<ColorModel> GetAllForGroup(long groupId) => _mapper.Map<IList<ColorModel>>(_repository.GetQuery().Where(c => c.GroupId == groupId));

        public void Add(ColorModel color)
        {
            Color colorToAdd = new();

            TransferValues(colorToAdd, color);

            _repository.Add(colorToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorModel color)
        {
            Color colorToUpdate = _repository.Get(color.Id);

            TransferValues(colorToUpdate, color);

            _repository.Update(colorToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(Color toColor, ColorModel fromColor)
        {
            toColor.Name = fromColor.Name;
            toColor.Code = fromColor.Code;
            toColor.GroupId = fromColor.Group.Id;
        }
    }
}
