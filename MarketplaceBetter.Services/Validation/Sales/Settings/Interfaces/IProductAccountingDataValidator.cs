using MarketplaceBetter.Domain.Model.Sales.Settings;
using MarketplaceBetter.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Validation.Sales.Settings.Interfaces
{
    public interface IProductAccountingDataValidator
    {
        ValidationResult Validate(ProductAccountingDataModel data);
    }
}
