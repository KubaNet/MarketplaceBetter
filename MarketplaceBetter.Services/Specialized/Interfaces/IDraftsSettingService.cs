using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
    public interface IDraftsSettingService
    {
        void SetDraftsSetting(bool showDrafts);

        bool GetDraftsSetting();
    }
}
