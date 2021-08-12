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
    public class MainPhraseStatusService : IMainPhraseStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MainPhraseStatus> _repository;
        private readonly IMapper _mapper;

        public MainPhraseStatusService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<MainPhraseStatus>();
            _mapper = mapper;
        }

        public MainPhraseStatusModel Get(long id) => _mapper.Map<MainPhraseStatusModel>(_repository.Get(id));

        public IList<MainPhraseStatusModel> GetAll() => _mapper.Map<IList<MainPhraseStatusModel>>(_repository.GetAll().OrderBy(g => g.Id));
    }
}
