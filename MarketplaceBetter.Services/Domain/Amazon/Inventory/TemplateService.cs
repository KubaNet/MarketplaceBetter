using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Amazon.Inventory.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Amazon.Inventory
{
    public class TemplateService : ITemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ParentInstance> _parentRepository;
        private readonly IRepository<Template> _templateRepository;
        private readonly string _templatesFolderPath;

        public TemplateService(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _parentRepository = unitOfWork.GetRepository<ParentInstance>();
            _templateRepository = unitOfWork.GetRepository<Template>();
            _templatesFolderPath = Path.Combine(environment.WebRootPath, "_templates");
        }

        public void Save(Stream fileStream, string fileName, long parentId)
        {
            ParentInstance parent = _parentRepository.Get(parentId);

            if (parent.Template != null)
            {
                Delete(parent.Template);
            }

            Create(parent, fileName);
            SaveFile(fileStream, fileName);
        }

        public void Delete(long id)
        {
            Template template = _templateRepository.Get(id);

            Delete(template);
        }

        private void Create(ParentInstance parent, string fileName)
        {
            Template template = new Template { FileName = fileName, Path = fileName };
            parent.Template = template;

            _parentRepository.Update(parent);
            _unitOfWork.Save();
        }

        private void Delete(Template template)
        {
            _templateRepository.Delete(template);
            _unitOfWork.Save();
        }

        private void SaveFile(Stream fileStream, string fileName)
        {
            string path = Path.Combine(_templatesFolderPath, fileName);
            using FileStream file = new FileStream(path, FileMode.Create);

            fileStream.CopyTo(file);
        }
    }
}
