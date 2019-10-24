using TmdbMovies.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TmdbMovies.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewMoviesPage : Page
    {
        public NewMoviesPageViewModel ViewModel { get; private set; }

        public NewMoviesPage()
        {
            this.InitializeComponent();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResetSearch();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.Search(true);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string vmId = NavigationService.GetNavigationVmId<NewMoviesPageViewModel>();
            ViewModel = NavigationService.RestoreState<NewMoviesPageViewModel>(vmId);
            
            DataContext = ViewModel;

            if (e.Parameter != null && (bool) e.Parameter)
            {
                await ViewModel.Search(true);
            }
        }
    }
}
