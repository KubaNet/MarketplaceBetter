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

        public const string NameNotUniqueForSelected = "There already exists a {0} with this Name for selected {1}.";

        public const string PropertyNotUnique = "There already exists a {0} with this {1}.";

        public const string PropertyNotUniqueForSelected = "There already exists a {0} with this {1} for selected {2}.";

        public const string EntityNotUniqueForSelected = "There already exists a {0} for selected {1}";
    }
}
