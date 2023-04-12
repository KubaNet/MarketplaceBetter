using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Base.Interfaces;
using MarketplaceBetter.Services.Model;
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

            User user = _repository.SingleOrDefault(u => u.Login == login);

            if (user == null)
            {
                return false;
            }

            return user.Password == password;
        }

        public void Login(string login)
        {
            User user = _repository.Single(u => u.Login == login);

            user.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            user.IpLastPing = DateTime.Now;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        public string GetCurrentUserLogin()
        {
            return GetCurrentUser()?.Login;
        }

        public BrandModel GetCurrentBrand()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<BrandModel>(user.CurrentBrand);
        }

        public void SetCurrentBrand(BrandModel brand)
        {
            User user = GetCurrentUser();
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
            User user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<InstanceModel>(user.CurrentInstance);
        }

        public void SetCurrentInstance(InstanceModel instance)
        {
            User user = GetCurrentUser();
            user.CurrentInstanceId = instance.Id;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        public bool IsSpecificInstance()
        {
            InstanceModel instance = GetCurrentInstance();

            return instance != null && instance.SystemName != InstanceEnum.All;
        }

        public bool IsExpanded(MenuItemEnum menu)
        {
            return GetExpandedMenuItems().Contains(menu);
        }

        public void SetExpanded(MenuItemEnum menu, bool expanded)
        {
            IList<MenuItemEnum> menuItems = GetExpandedMenuItems();

            if (expanded)
            {
                if (menuItems.Contains(menu))
                {
                    return;
                }
                else
                {
                    menuItems.Add(menu);
                }
            }
            else
            {
                if (menuItems.Contains(menu))
                {
                    menuItems.Remove(menu);
                }
                else
                {
                    return;
                }
            }

            SaveExpandedMenuItems(menuItems);
        }

        public bool ShowDrafts()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            return user.ShowDrafts;
        }

        public void SetShowDrafts(bool showDrafts)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return;
            }

            user.ShowDrafts = showDrafts;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        public bool ShowWithdrawn()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            return user.ShowWithdrawn;
        }

        public void SetShowWithdrawn(bool showWithdrawn)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return;
            }

            user.ShowWithdrawn = showWithdrawn;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        private User GetCurrentUser()
        {
            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return _repository.SingleOrDefault(u => u.IpAddress == ipAddress);
        }

        private IList<MenuItemEnum> GetExpandedMenuItems()
        {
            IList<MenuItemEnum> menuItems = new List<MenuItemEnum>();

            IList<string> menuItemsStrings = GetCurrentUser().ExpandedMenu.Split(';');
            foreach (string menuItemString in menuItemsStrings)
            {
                if (string.IsNullOrWhiteSpace(menuItemString))
                {
                    continue;
                }

                MenuItemEnum menuItem = Enum.Parse<MenuItemEnum>(menuItemString);

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        private void SaveExpandedMenuItems(IList<MenuItemEnum> menuItems)
        {
            User user = GetCurrentUser();
            user.ExpandedMenu = string.Join(";", menuItems.Cast<int>());

            _repository.Update(user);
            _unitOfWork.Save();
        }
    }
}
