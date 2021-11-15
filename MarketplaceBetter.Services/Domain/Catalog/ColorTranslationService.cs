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
    public class ColorTranslationService : IColorTranslationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ColorTranslation> _repository;
        private readonly IMapper _mapper;

        public ColorTranslationService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ColorTranslation>();
            _mapper = mapper;
        }

        public ColorTranslationModel Get(long id) => _mapper.Map<ColorTranslationModel>(_repository.Get(id));

        public IList<ColorTranslationModel> GetAll() => _mapper.Map<IList<ColorTranslationModel>>(_repository.GetAll());

        public void Add(ColorTranslationModel translation)
        {
            ColorTranslation translationToAdd = new();

            TransferValues(translationToAdd, translation);

            _repository.Add(translationToAdd);
            _unitOfWork.Save();
        }

        public void Update(ColorTranslationModel translation)
        {
            ColorTranslation translationToUpdate = _repository.Get(translation.Id);

            TransferValues(translationToUpdate, translation);

            _repository.Update(translationToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(ColorTranslation toTranslation, ColorTranslationModel fromTranslation)
        {
            toTranslation.InstanceId = fromTranslation.Instance.Id;
            toTranslation.ColorId = fromTranslation.Color.Id;
            toTranslation.Translation = fromTranslation.Translation;
            toTranslation.Mapping = fromTranslation.Mapping;
        }
    }
}
