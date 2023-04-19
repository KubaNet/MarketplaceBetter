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
                DeleteFile(parent.Template);
                DeleteTemplate(parent.Template);
            }

            string folderPath = $@"{parent.Product.Brand.Name}\{parent.Product.Code}\{parent.Instance.Name}";

            Create(parent, fileName, folderPath);
            SaveFile(fileStream, fileName, folderPath);
        }

        public Stream Download(long id)
        {
            Template template = _templateRepository.Get(id);

            string fullFilePath = Path.Combine(_templatesFolderPath, template.Path, template.FileName);
            FileStream file = new FileStream(fullFilePath, FileMode.Open);

            return file;
        }

        public void Delete(long id)
        {
            Template template = _templateRepository.Get(id);

            DeleteFile(template);
            DeleteTemplate(template);
        }

        private void Create(ParentInstance parent, string fileName, string folderPath)
        {
            Template template = new Template { FileName = fileName, Path = folderPath };

            parent.Template = template;

            _parentRepository.Update(parent);
            _unitOfWork.Save();
        }

        private void DeleteTemplate(Template template)
        {
            _templateRepository.Delete(template);
            _unitOfWork.Save();
        }

        private void SaveFile(Stream fileStream, string fileName, string folderPath)
        {
            string serverFolderPath = Path.Combine(_templatesFolderPath, folderPath);
            Directory.CreateDirectory(serverFolderPath);

            string fullFilePath = Path.Combine(serverFolderPath, fileName);
            using FileStream file = new FileStream(fullFilePath, FileMode.Create);

            fileStream.CopyTo(file);
        }

        private void DeleteFile(Template template)
        {
            string fullFilePath = Path.Combine(_templatesFolderPath, template.Path, template.FileName);

            File.Delete(fullFilePath);
        }
    }
}
