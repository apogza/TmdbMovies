using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace TmdbMovies.Helpers
{
    public static class DialogService
    {
        public static async Task ShowMessageDialog(string title, string message)
        {
            var messageDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            await messageDialog.ShowAsync(ContentDialogPlacement.Popup);
        }
    }
}
