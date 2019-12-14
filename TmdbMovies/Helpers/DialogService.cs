using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace TmdbMovies.Helpers
{
    public static class DialogService
    {
        public static async Task ShowSimpleMessageDialog(string title, string message, ResourceLoader resourceLoader = null)
        {
            if (resourceLoader == null)
            {
                resourceLoader = ResourceLoader.GetForCurrentView();
            }

            string okLabel = resourceLoader.GetString("OkLabel");

            var messageDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = okLabel,
            };

            await messageDialog.ShowAsync(ContentDialogPlacement.Popup);
        }

        public static async Task ShowErrorMessageDialog(string errorTitleKey, string messageKey)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            string errorMessage = resourceLoader.GetString(errorTitleKey);
            string errorTitle = resourceLoader.GetString(messageKey);

            await ShowSimpleMessageDialog(errorTitle, errorMessage);
        }
    }
}
