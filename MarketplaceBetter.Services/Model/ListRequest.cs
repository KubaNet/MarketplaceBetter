using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Model
{
    public class ListRequest
    {
        public string SearchString { get; set; }

        public string SortBy { get; set; }

        public SortDirection SortDirection { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
