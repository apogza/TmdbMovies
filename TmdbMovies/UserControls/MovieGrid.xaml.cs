using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TmdbMovies.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TmdbMovies.UserControls
{
    public sealed partial class MovieGrid : UserControl
    {
        public static DependencyProperty MoviesProperty = 
            DependencyProperty.Register("Movies", typeof(IEnumerable<Movie>), typeof(MovieGrid), new PropertyMetadata(null));

        public IEnumerable<Movie> Movies
        {
            get { return (IEnumerable<Movie>)GetValue(MovieGrid.MoviesProperty); }
            set { SetValue(MovieGrid.MoviesProperty, value); }
        }

        public MovieGrid()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Movie selectedMovie)
            {
                NavigationService.NavigateTo("MoviePage", selectedMovie);
            }
        }
    }
}
