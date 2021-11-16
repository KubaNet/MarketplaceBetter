using AutoMapper;
using MarketplaceBetter.Domain.Entities.Catalog;
using MarketplaceBetter.Domain.Model.Catalog;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Infrastructure.Exceptions;
using MarketplaceBetter.Infrastructure.Extensions;
using MarketplaceBetter.Services.Domain.Catalog.Interfaces;
using MarketplaceBetter.Services.Helpers;
using MarketplaceBetter.Services.Model;
using MudBlazor;
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

        public int CountForListRequest(ListRequest request)
        {
            IQueryable<ColorTranslation> translations = _repository.GetQuery();

            ApplyFilter(translations, request);

            return translations.Count();
        }

        public IList<ColorTranslationModel> GetForListRequest(ListRequest request)
        {
            IQueryable<ColorTranslation> translations = _repository.GetQuery();

            translations = ApplyFilter(translations, request);
            translations = ApplySorting(translations, request);
            translations = ApplyPaging(translations, request);

            return _mapper.Map<IList<ColorTranslationModel>>(translations);
        }

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

        private IQueryable<ColorTranslation> ApplyFilter(IQueryable<ColorTranslation> translations, ListRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchString))
            {
                return translations;
            }

            IList<string> searchStrings = request.SearchString.SplitForFiltering();

            foreach (string searchString in searchStrings)
            {
                string[] searchFieldNames = new[] { "id", "instance", "translation", "mapping", "color", "color_group", "brand" };
                SearchField searchField = SearchFieldExtractor.ExtractFrom(searchString, searchFieldNames);

                if (searchField != null)
                {
                    translations = searchField.Name switch
                    {
                        "id" => translations.Where(t => t.Id == searchField.Value.ParseToIntOrDefault()),
                        "instance" => translations.Where(t => t.Instance.Name.Contains(searchField.Value)),
                        "translation" => translations.Where(t => t.Translation.Contains(searchField.Value)),
                        "mapping" => translations.Where(t => t.Mapping.Contains(searchField.Value)),
                        "color" => translations.Where(t => t.Color.Name.Contains(searchField.Value)),
                        "color_group" => translations.Where(t => t.Color.Group.Name.Contains(searchField.Value)),
                        "brand" => translations.Where(t => t.Color.Group.Brand.Name.Contains(searchField.Value)),
                        _ => throw new UnrecognizedSearchFieldException(searchField.Name)
                    };
                }
                else
                {
                    translations = translations.Where(t => t.Id == searchString.ParseToIntOrDefault()
                        || t.Instance.Name.Contains(searchString)
                        || t.Translation.Contains(searchString)
                        || t.Mapping.Contains(searchString)
                        || t.Color.Name.Contains(searchString)
                        || t.Color.Group.Name.Contains(searchString)
                        || t.Color.Group.Brand.Name.Contains(searchString));
                }
            }

            return translations;
        }

        private IQueryable<ColorTranslation> ApplySorting(IQueryable<ColorTranslation> translations, ListRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                translations = request.SortBy switch
                {
                    "id" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Id) : translations.OrderByDescending(t => t.Id),
                    "instance" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Instance.Name) : translations.OrderByDescending(t => t.Instance.Name),
                    "translation" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Translation) : translations.OrderByDescending(t => t.Translation),
                    "mapping" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Mapping) : translations.OrderByDescending(t => t.Mapping),
                    "color" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Color.Name) : translations.OrderByDescending(t => t.Color.Name),
                    "color_group" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Color.Group.Name) : translations.OrderByDescending(t => t.Color.Group.Name),
                    "brand" => request.SortDirection == SortDirection.Ascending ? translations.OrderBy(t => t.Color.Group.Brand.Name) : translations.OrderByDescending(t => t.Color.Group.Brand.Name),
                    _ => throw new UnrecognizedSortingException<ListRequest>(request.SortBy)
                };
            }

            return translations;
        }

        private IQueryable<ColorTranslation> ApplyPaging(IQueryable<ColorTranslation> translations, ListRequest request)
        {
            return translations.Skip(request.Page * request.PageSize).Take(request.PageSize);
        }
    }
}
