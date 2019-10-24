using System;
using TmdbMovies.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TmdbMovies.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchMoviesPage : Page
    {
        public SearchMoviesPageViewModel ViewModel { get; set; }

        public SearchMoviesPage()
        {
            this.InitializeComponent();
        }

        private async void SearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            await ViewModel.Search(true);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string viewModelId = NavigationService.GetNavigationVmId<SearchMoviesPageViewModel>();

            ViewModel = NavigationService.RestoreState<SearchMoviesPageViewModel>(viewModelId);
            DataContext = ViewModel;
        }
    }
}
