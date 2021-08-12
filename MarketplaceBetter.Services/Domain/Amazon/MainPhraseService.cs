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
    public class MainPhraseService : IMainPhraseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MainPhrase> _repository;
        private readonly IMapper _mapper;

        public MainPhraseService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<MainPhrase>();
            _mapper = mapper;
        }

        public MainPhraseModel Get(long id) => _mapper.Map<MainPhraseModel>(_repository.Get(id));

        public IList<MainPhraseModel> GetAll() => _mapper.Map<IList<MainPhraseModel>>(_repository.GetAll().OrderBy(g => g.Name));

        public IList<MainPhraseModel> GetForAnalysis(long analysisId) => _mapper.Map<IList<MainPhraseModel>>(_repository.GetQuery().Where(p => p.AnalysisId == analysisId).OrderBy(g => g.Name));

        public void Add(MainPhraseModel phrase)
        {
            MainPhrase phraseToAdd = new();

            TransferValues(phraseToAdd, phrase);

            _repository.Add(phraseToAdd);
            _unitOfWork.Save();
        }

        public void Update(MainPhraseModel phrase)
        {
            MainPhrase phraseToUpdate = _repository.Get(phrase.Id);

            TransferValues(phraseToUpdate, phrase);

            _repository.Update(phraseToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(MainPhrase toPhrase, MainPhraseModel fromPhrase)
        {
            toPhrase.Name = fromPhrase.Name;
            toPhrase.Description = fromPhrase.Description;
            toPhrase.StatusId = fromPhrase.Status.Id;
            toPhrase.AnalysisId = fromPhrase.Analysis.Id;
        }
    }
}
