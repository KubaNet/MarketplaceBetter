using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Model.Amazon.Keywords;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Amazon.Keywords.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon
{
    public class ResearchTargetService : IResearchTargetService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ResearchTarget> _repository;
        private readonly IRepository<ResearchTargetStatus> _statusRepository;

        public ResearchTargetService(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<ResearchTarget>();
            _statusRepository = unitOfWork.GetRepository<ResearchTargetStatus>();
        }

        public ResearchTargetModel Get(long id) => _mapper.Map<ResearchTargetModel>(_repository.Get(id));

        public IList<ResearchTargetModel> GetAll() => _mapper.Map<IList<ResearchTargetModel>>(_repository.GetQuery().OrderBy(r => r.Name));

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ResearchTarget> targets = _repository.GetQuery();

            ApplyFilter(targets, request);

            return targets.Count();
        }

        public IList<ResearchTargetModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ResearchTarget> targets = _repository.GetQuery();

            targets = ApplyFilter(targets, request);
            targets = ApplySorting(targets, request);
            targets = ApplyPaging(targets, request);

            return _mapper.Map<IList<ResearchTargetModel>>(targets);
        }

        public void Add(ResearchTargetModel target)
        {
            ResearchTarget targetToAdd = new();

            TransferValues(targetToAdd, target);

            _repository.Add(targetToAdd);
            _unitOfWork.Save();
        }

        public void AddFromFile(MemoryStream file, AddResearchTargetsModel model)
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };

            using var reader = new StreamReader(file);
            using var csv = new CsvReader(reader, config);

            csv.Read();
            if (model.Source.SystemName == ResearchTargetSourceEnum.AmazonSearchTerms)
            {
                csv.Read();
            }
            csv.ReadHeader();

            while (csv.Read())
            {
                string name = GetTargetName(model.Source.SystemName, csv);

                ResearchTarget target = _repository.SingleOrDefault(t => t.ResearchId == model.Research.Id && t.Name == name);

                if (target == null)
                {
                    target = new ResearchTarget();
                    target.ResearchId = model.Research.Id;
                    target.Name = name;
                    target.Status = _statusRepository.Single(s => s.SystemName == ResearchTargetStatusEnum.Included);

                    SetValue(target, model.Source.SystemName, csv);

                    _repository.Add(target);
                }
                else
                {
                    SetValue(target, model.Source.SystemName, csv);

                    _repository.Update(target);
                }
            }

            _unitOfWork.Save();
        }

        public void Update(ResearchTargetModel target)
        {
            ResearchTarget targetToUpdate = _repository.Get(target.Id);

            TransferValues(targetToUpdate, target);

            _repository.Update(targetToUpdate);
            _unitOfWork.Save();
        }

        private void SetValue(ResearchTarget target, ResearchTargetSourceEnum source, CsvReader csv)
        {
            if (source == ResearchTargetSourceEnum.Helium10)
            {
                target.Helium10Value = int.Parse(csv.GetField("Search Volume").Replace(",", string.Empty));
            }
            else if (source == ResearchTargetSourceEnum.AmazonSearchTerms)
            {
                target.Helium10Value = int.Parse(csv.GetField("Search Frequency Rank").Replace(",", string.Empty));
            }
            else
            {
                throw new UnrecognizedEnumValue<ResearchTargetSourceEnum>(source);
            }
        }

        private string GetTargetName(ResearchTargetSourceEnum source, CsvReader csv)
        {
            if (source == ResearchTargetSourceEnum.Helium10)
            {
                return csv.GetField("Keyword Phrase");
            }
            else if (source == ResearchTargetSourceEnum.AmazonSearchTerms)
            {
                return csv.GetField("Search Term");
            }
            else
            {
                throw new UnrecognizedEnumValue<ResearchTargetSourceEnum>(source);
            }
        }

        private void TransferValues(ResearchTarget toResearch, ResearchTargetModel fromResearch)
        {
            toResearch.Name = fromResearch.Name;
            toResearch.ResearchId = fromResearch.Research.Id;
        }

        private IQueryable<ResearchTarget> ApplyFilter(IQueryable<ResearchTarget> targets, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return targets;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "name", "research" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    targets = searchField.Name switch
                    {
                        "id" => targets.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "name" => targets.Where(t => t.Name.Contains(searchField.Value)),
                        "research" => targets.Where(t => t.Research.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    targets = targets.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Name.Contains(searchString)
                        || t.Research.Name.Contains(searchString));
                }
            }

            return targets;
        }

        private IQueryable<ResearchTarget> ApplySorting(IQueryable<ResearchTarget> targets, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                targets = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Id) : targets.OrderByDescending(t => t.Id),
                    "name" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Name) : targets.OrderByDescending(t => t.Name),
                    "h10" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Helium10Value) : targets.OrderByDescending(t => t.Helium10Value),
                    "ast" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.AmazonSearchTermsValue) : targets.OrderByDescending(t => t.AmazonSearchTermsValue),
                    "research" => request.SortDirection == SortDirection.Ascending ? targets.OrderBy(t => t.Research.Name) : targets.OrderByDescending(t => t.Research.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return targets;
        }

        private IQueryable<ResearchTarget> ApplyPaging(IQueryable<ResearchTarget> targets, ListRequest request)
        {
            return targets.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
