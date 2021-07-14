using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Validation
{
    public static class ValidationMessages
    {
        public const string NameNotUnique = "There already exists a {0} with this Name.";

        public const string PropertyNotUnique = "There already exists a {0} with this {1}.";
    }
}
