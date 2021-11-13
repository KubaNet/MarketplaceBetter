using MarketplaceBetter.Services.Model;
using MudBlazor;

namespace MarketplaceBetter.Web.Extensions
{
    public static class TableStateExtensions
    {
        public static void PopulateListRequest(this TableState state, ListRequest request)
        {
            request.SortBy = state.SortLabel;
            request.SortDirection = state.SortDirection;
            request.Page = state.Page;
            request.PageSize = state.PageSize;
        }
    }
}
