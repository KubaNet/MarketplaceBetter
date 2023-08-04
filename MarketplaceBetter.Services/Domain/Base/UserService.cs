using AutoMapper;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
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
        private readonly TimeSpan LOGIN_TIMEOUT = TimeSpan.FromMinutes(30);

        private long? CurrentStatusIdChache;
        private long? CurrentBrandIdCache;
        private long? CurrentSizeIdCache;
        private long? CurrentCollectionIdCache;
        private long CurrentInstanceIdCache;
        private bool HideDraftsCache;
        private bool HideWithdrawnCache;
        private string ExpandedMenuCache;

        public UserService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<User>();
            _httpContextAccessor = httpContextAccessor;

            UpdateSettingsCache();
        }

        private void UpdateSettingsCache()
        {
            User user = GetCurrentUser();

            UpdateSettingsCache(user);
        }

        public IList<UserModel> GetAll() => _mapper.Map<IList<UserModel>>(_repository.GetAll());

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

            UpdateSettingsCache(user);
        }

        public void Logout()
        {
            User user = GetCurrentUser();
            
            if (user == null)
            {
                return;
            }

            user.IpAddress = null;
            user.IpLastPing = null;

            _repository.Update(user);
            _unitOfWork.Save();
        }

        public bool LoginExpired()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return true;
            }

            return false;
        }

        public bool SettingsChanged()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            if (user.CurrentStatusId != CurrentStatusIdChache)
            {
                return true;
            }    

            if (user.CurrentBrandId != CurrentBrandIdCache)
            {
                return true;
            }

            if (user.CurrentSizeId != CurrentSizeIdCache)
            {
                return true;
            }

            if (user.CurrentCollectionId != CurrentCollectionIdCache)
            {
                return true;
            }

            if (user.CurrentInstanceId != CurrentInstanceIdCache)
            {
                return true;
            }

            if (user.HideDrafts != HideDraftsCache)
            {
                return true;
            }

            if (user.HideWithdrawn != HideWithdrawnCache)
            {
                return true;
            }

            if (user.ExpandedMenu != ExpandedMenuCache)
            {
                return true;
            }

            return false;
        }

        public string GetCurrentUserLogin()
        {
            return GetCurrentUser()?.Login;
        }

        public EntityStatusModel GetCurrentStatus()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<EntityStatusModel>(user.CurrentStatus);
        }

        public void SetCurrentStatus(EntityStatusModel status)
        {
            User user = GetCurrentUser();

            if (status == null || status.Id == 0)
            {
                user.CurrentStatusId = null;
            }
            else
            {
                user.CurrentStatusId = status.Id;
            }

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        public bool IsSpecificStatus()
        {
            EntityStatusModel status = GetCurrentStatus();

            return status != null;
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

            if (brand == null || brand.Id == 0)
            {
                user.CurrentBrandId = null;
            }
            else
            {
                user.CurrentBrandId = brand.Id;
            }

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        public bool IsSpecificBrand()
        {
            BrandModel brand = GetCurrentBrand();

            return brand != null;
        }

        public StandardSizeModel GetCurrentSize()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<StandardSizeModel>(user.CurrentSize);
        }

        public void SetCurrentSize(StandardSizeModel size)
        {
            User user = GetCurrentUser();

            if (size == null || size.Id == 0)
            {
                user.CurrentSizeId = null;
            }
            else
            {
                user.CurrentSizeId = size.Id;
            }

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        public bool IsSpecificSize()
        {
            StandardSizeModel size = GetCurrentSize();

            return size != null;
        }

        public CollectionModel GetCurrentCollection()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<CollectionModel>(user.CurrentCollection);
        }

        public void SetCurrentCollection(CollectionModel collection)
        {
            User user = GetCurrentUser();

            if (collection == null || collection.Id == 0)
            {
                user.CurrentCollectionId = null;
            }
            else
            {
                user.CurrentCollectionId = collection.Id;
            }

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        public bool IsSpecificCollection()
        {
            CollectionModel collection = GetCurrentCollection();

            return collection != null;
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

            UpdateSettingsCache(user);
        }

        public bool IsSpecificInstance()
        {
            InstanceModel instance = GetCurrentInstance();

            return instance != null && instance.SystemName != InstanceEnum.All;
        }

        public bool IsExpanded(MenuItemEnum menu)
        {
            IList<MenuItemEnum> menuItems = GetExpandedMenuItems();

            if (menuItems == null)
            {
                return false;
            }

            return menuItems.Contains(menu);
        }

        public void SetExpanded(MenuItemEnum menu, bool expanded)
        {
            IList<MenuItemEnum> menuItems = GetExpandedMenuItems();

            if (menuItems == null)
            {
                return;
            }

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

        public bool HideDrafts()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            return user.HideDrafts;
        }

        public void SetHideDrafts(bool hideDrafts)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return;
            }

            user.HideDrafts = hideDrafts;

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        public bool HideWithdrawn()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            return user.HideWithdrawn;
        }

        public void SetHideWithdrawn(bool hideWithdrawn)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return;
            }

            user.HideWithdrawn = hideWithdrawn;

            _repository.Update(user);
            _unitOfWork.Save();

            UpdateSettingsCache(user);
        }

        private void UpdateSettingsCache(User user)
        {
            if (user == null)
            {
                return;
            }

            CurrentStatusIdChache = user.CurrentStatusId;
            CurrentBrandIdCache = user.CurrentBrandId;
            CurrentSizeIdCache = user.CurrentSizeId;
            CurrentCollectionIdCache = user.CurrentCollectionId;
            CurrentInstanceIdCache = user.CurrentInstanceId;
            HideDraftsCache = user.HideDrafts;
            HideWithdrawnCache = user.HideWithdrawn;
            ExpandedMenuCache = user.ExpandedMenu;
        }

        private User GetCurrentUser()
        {
            string ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString();
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return null;
            }

            User user = _repository.SingleOrDefault(u => u.IpAddress == ipAddress);
            if (user == null)
            {
                return null;
            }

            TimeSpan timeFromLastPing = DateTime.Now - user.IpLastPing.Value;
            if (timeFromLastPing > LOGIN_TIMEOUT)
            {
                return null;
            }

            _repository.Reload(user);

            user.IpLastPing = DateTime.Now;

            _repository.Update(user);
            _unitOfWork.Save();

            return user;
        }

        private IList<MenuItemEnum> GetExpandedMenuItems()
        {
            IList<MenuItemEnum> menuItems = new List<MenuItemEnum>();

            User user = GetCurrentUser();
            if (user == null)
            {
                return null;
            }

            IList<string> menuItemsStrings = user.ExpandedMenu.Split(';');
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

            UpdateSettingsCache(user);
        }
    }
}
