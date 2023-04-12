using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Model;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<User>();
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsCorrectPassword(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return true;
        }

        public void Login(string login, string password)
        {

        }

        public BrandModel GetCurrentBrand()
        {
            User user = GetUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<BrandModel>(user.CurrentBrand);
        }

        public void SetCurrentBrand(BrandModel brand)
        {
            User user = GetUser();
            user.CurrentBrandId = brand.Id;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        public bool IsSpecificBrand()
        {
            BrandModel brand = GetCurrentBrand();

            return brand != null;
        }

        public InstanceModel GetCurrentInstance()
        {
            throw new NotImplementedException();
        }

        public bool IsExpanded(MenuItemEnum menu)
        {
            throw new NotImplementedException();
        }

        public bool IsSpecificInstance()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentInstance(InstanceModel brand)
        {
            throw new NotImplementedException();
        }

        public void SetExpanded(MenuItemEnum menu, bool expanded)
        {
            throw new NotImplementedException();
        }

        public void SetShowDrafts(bool showDrafts)
        {
            throw new NotImplementedException();
        }

        public void SetShowWithdrawn(bool showWithdrawn)
        {
            throw new NotImplementedException();
        }

        public bool ShowDrafts()
        {
            throw new NotImplementedException();
        }

        public bool ShowWithdrawn()
        {
            throw new NotImplementedException();
        }

        private User GetUser()
        {
            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return _repository.SingleOrDefault(u => u.IpAddress == ipAddress);
        }
    }
}
