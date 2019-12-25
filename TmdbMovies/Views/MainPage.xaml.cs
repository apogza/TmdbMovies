using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using TmdbMovies.Models;
using Windows.ApplicationModel.Resources;
using Windows.Devices.Input;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Web.UI.Interop;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TmdbMovies.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            InitNavigation();
        }

        private void InitNavigation()
        {
            NavigationService.ContentFrame = ContentFrame;
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;

            if (item?.Tag == null)
            {
                return;
            }

            NavigationService.NavigateTo(item.Tag.ToString());            
        }

        private void NavigationView_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void MainPage_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                PointerPoint ptrPt = e.GetCurrentPoint(this);

                if (ptrPt.Properties.IsXButton1Pressed && ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                }

                if (ptrPt.Properties.IsXButton2Pressed && ContentFrame.CanGoForward)
                {
                    ContentFrame.GoForward();
                }
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await CheckInternetConnection();
            await LoadApiKey();
            await CheckTmdbAccess();

            MainNavigationView.SelectedItem = MainNavigationView.MenuItems.First();

            NavigationService.NavigateTo(typeof(NewMoviesPage), true);
        }

        private void MainPage_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Left && e.KeyStatus.IsMenuKeyDown && ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }

            if (e.Key == VirtualKey.Right && e.KeyStatus.IsMenuKeyDown && this.ContentFrame.CanGoForward)
            {
                ContentFrame.GoForward();
            }
        }

        private async Task LoadApiKey()
        {
            string filePath = TmdbConstants.ApiKeyFile;

            if (!File.Exists(filePath))
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();
                string errorMessage = resourceLoader.GetString("MainPage/MissingApiError");
                string errorTitle = resourceLoader.GetString("MainPage/ErrorLabel");

                await DialogService.ShowSimpleMessageDialog(errorTitle, errorMessage);
                Application.Current.Exit();
            }

            string key = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(key))
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();
                string errorMessage = resourceLoader.GetString("MainPage/ApiError");
                string errorTitle = resourceLoader.GetString("MainPage/ErrorLabel");

                await DialogService.ShowSimpleMessageDialog(errorTitle, errorMessage);
                Application.Current.Exit();
            }

            TmdbConstants.TmdbKey = key;
        }

        private async Task CheckTmdbAccess()
        {
            try
            {
                string testQuery = $"movie/550?api_key={TmdbConstants.TmdbKey}";

                RestClient restClient = new RestClient(TmdbConstants.BaseUri);
                await restClient.GetEntity<Movie>(testQuery);
            }
            catch (InvalidOperationException)
            {
                await DialogService.ShowErrorMessageDialog("ErrorLabel", "ApiError");
                Application.Current.Exit();
            }
        }

        private async Task CheckInternetConnection()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();
                string errorMessage = resourceLoader.GetString("NetworkError");
                string errorTitle = resourceLoader.GetString("ErrorLabel");

                await DialogService.ShowSimpleMessageDialog(errorTitle, errorMessage);
                
                Application.Current.Exit();
            }
        }
    }
}
