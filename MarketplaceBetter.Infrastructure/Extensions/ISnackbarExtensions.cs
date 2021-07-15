using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Extensions
{
    public static class ISnackbarExtensions
    {
        public static Snackbar AddSuccess(this ISnackbar snackbar, string message)
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            snackbar.Configuration.ShowTransitionDuration = 500;
            snackbar.Configuration.HideTransitionDuration = 500;
            snackbar.Configuration.ShowCloseIcon = false;
            snackbar.Configuration.VisibleStateDuration = 3000;
            snackbar.Configuration.SnackbarVariant = Variant.Filled;

            return snackbar.Add(message, Severity.Success);
        }
    }
}
