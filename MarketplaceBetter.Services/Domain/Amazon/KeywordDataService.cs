using AutoMapper;
using MarketplaceBetter.Domain.Entities.Amazon;
using MarketplaceBetter.Domain.Model.Amazon;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class KeywordDataService : IKeywordDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<KeywordData> _repository;
        private readonly IMapper _mapper;

        public KeywordDataService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<KeywordData>();
            _mapper = mapper;
        }

        public KeywordDataModel Get(long id) => _mapper.Map<KeywordDataModel>(_repository.Get(id));

        public IList<KeywordDataModel> GetAll() => _mapper.Map<IList<KeywordDataModel>>(_repository.GetAll().OrderByDescending(d => d.Id));

        public void Add(KeywordDataModel data)
        {
            KeywordData dataToAdd = new();

            TransferValues(dataToAdd, data);

            _repository.Add(dataToAdd);
            _unitOfWork.Save();
        }

        public void Update(KeywordDataModel data)
        {
            KeywordData dataToUpdate = _repository.Get(data.Id);

            TransferValues(dataToUpdate, data);

            _repository.Update(dataToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(KeywordData toData, KeywordDataModel fromData)
        {
            toData.MainPhraseId = fromData.MainPhrase.Id;
            toData.SourceId = fromData.Source.Id;
        }
    }
}
