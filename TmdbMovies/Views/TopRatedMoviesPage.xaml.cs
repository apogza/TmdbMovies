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

    public sealed partial class TopRatedMoviesPage : Page
    {

        public TopRatedMoviesPageViewModel ViewModel { get; set; }

        public TopRatedMoviesPage()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = NavigationService.RestoreState<TopRatedMoviesPageViewModel>();
            DataContext = ViewModel;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }
    }
}
