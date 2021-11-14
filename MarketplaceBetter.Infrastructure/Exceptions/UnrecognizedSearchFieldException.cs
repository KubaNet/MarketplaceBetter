using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Exceptions
{
    public class UnrecognizedSearchFieldException : Exception
    {
        public UnrecognizedSearchFieldException(string fieldName) : base(GetMessage(fieldName))
        {

        }

        private static string GetMessage(string fieldName)
        {
            return string.Format("Unrecognized search field name: {0}.", fieldName);
        }
    }
}
