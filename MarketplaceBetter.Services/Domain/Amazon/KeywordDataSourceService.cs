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
    public class KeywordDataSourceService : IKeywordDataSourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<KeywordDataSource> _repository;
        private readonly IMapper _mapper;

        public KeywordDataSourceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<KeywordDataSource>();
            _mapper = mapper;
        }

        public KeywordDataSourceModel Get(long id) => _mapper.Map<KeywordDataSourceModel>(_repository.Get(id));

        public IList<KeywordDataSourceModel> GetAll() => _mapper.Map<IList<KeywordDataSourceModel>>(_repository.GetAll().OrderBy(g => g.Id));
    }
}
