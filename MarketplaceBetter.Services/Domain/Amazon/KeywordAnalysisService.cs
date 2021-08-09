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
    public class KeywordAnalysisService : IKeywordAnalysisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<KeywordAnalysis> _repository;
        private readonly IMapper _mapper;

        public KeywordAnalysisService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<KeywordAnalysis>();
            _mapper = mapper;
        }

        public KeywordAnalysisModel Get(long id)
        {
            KeywordAnalysis analysis = _repository.Get(id);

            return _mapper.Map<KeywordAnalysisModel>(analysis);
        }

        public IList<KeywordAnalysisModel> GetAll()
        {
            IList<KeywordAnalysisModel> analyses = _mapper.Map<IList<KeywordAnalysisModel>>(_repository.GetAll());

            return analyses;
        }

        public void Add(KeywordAnalysisModel analysis)
        {
            KeywordAnalysis analysisToAdd = new();

            TransferValues(analysisToAdd, analysis);

            _repository.Add(analysisToAdd);
            _unitOfWork.Save();
        }

        public void Update(KeywordAnalysisModel analysis)
        {
            KeywordAnalysis analysisToUpdate = _repository.Get(analysis.Id);

            TransferValues(analysisToUpdate, analysis);

            _repository.Update(analysisToUpdate);
            _unitOfWork.Save();
        }

        private void TransferValues(KeywordAnalysis toKeywordAnalysis, KeywordAnalysisModel fromKeywordAnalysis)
        {
            toKeywordAnalysis.Name = fromKeywordAnalysis.Name;
            toKeywordAnalysis.Description = fromKeywordAnalysis.Description;
            toKeywordAnalysis.InstanceId = fromKeywordAnalysis.Instance.Id;
        }
    }
}
