using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Validation
{
    public class ValidationError
    {
        public ValidationError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; private set; }

        public string Message { get; private set; }
    }
}
