using TmdbMovies.Models;
using TmdbMovies.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TmdbMovies.UserControls
{
    public sealed partial class SearchMovieGrid : UserControl
    {
        public BaseSearchMovieViewModel  MovieSearchViewModel
        {
            get { return (BaseSearchMovieViewModel )GetValue(MovieSearchViewModelProperty); }
            set { SetValue(MovieSearchViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MovieSearchViewModelProperty =
            DependencyProperty.Register("MovieSearchViewModelProperty", typeof(BaseSearchMovieViewModel ), typeof(SearchMovieGrid), null);

        public SearchMovieGrid()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Paginator_OnOnPageChange(object sender, int e)
        {
            MovieSearchViewModel.OnPageChange(e);
        }
    }
}
