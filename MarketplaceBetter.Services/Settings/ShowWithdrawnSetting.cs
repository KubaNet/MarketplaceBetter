using MarketplaceBetter.Services.Settings.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Settings
{
    public class ShowWithdrawnSetting : IShowWithdrawnSetting
    {
        private const string STORAGE_KEY = "ShowWithdrawn";
        private readonly IConfiguration _configuration;

        public ShowWithdrawnSetting(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ShowWithdrawn()
        {
            return _configuration.GetValue<bool>(STORAGE_KEY);
        }

        public void SetWithdrawnSetting(bool showWithdrawn)
        {
            _configuration[STORAGE_KEY] = showWithdrawn.ToString();
        }
    }
}
