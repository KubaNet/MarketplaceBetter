using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized
{
    public class DraftsSettingService : IDraftsSettingService
    {
        private const string STORAGE_KEY = "ShowDrafts";
        private readonly IConfiguration _configuration;

        public DraftsSettingService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool GetDraftsSetting()
        {
            return _configuration.GetValue<bool>(STORAGE_KEY);
        }

        public void SetDraftsSetting(bool showDrafts)
        {
            _configuration[STORAGE_KEY] = showDrafts.ToString();
        }
    }
}
