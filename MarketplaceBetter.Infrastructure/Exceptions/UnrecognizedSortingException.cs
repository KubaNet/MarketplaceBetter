using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Exceptions
{
    public class UnrecognizedSortingException<T> : Exception
    {
        public UnrecognizedSortingException(string sortBy) : base(GetMessage(sortBy))
        {

        }

        private static string GetMessage(string sortBy)
        {
            return string.Format("Unrecognized value of sorting. Value: {0}. Type of list request: {1}.", sortBy, typeof(T).ShortDisplayName());
        }
    }
}
