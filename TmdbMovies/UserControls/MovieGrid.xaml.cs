using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using TmdbMovies.Models;
using TmdbMovies.ViewModels;
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
        public BaseMovieSearchViewModel MovieSearchViewModel
        {
            get { return (BaseMovieSearchViewModel)GetValue(MovieSearchViewModelProperty); }
            set { SetValue(MovieSearchViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MovieSearchViewModelProperty =
            DependencyProperty.Register("MovieSearchViewModelProperty", typeof(BaseMovieSearchViewModel), typeof(MovieGrid), null);

        public MovieGrid()
        {
            this.InitializeComponent();

        }

        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            string newValue = new string(sender.Text.Where(char.IsDigit).ToArray());
            sender.Text = string.IsNullOrWhiteSpace(newValue) ? "1" : newValue;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Movie selectedMovie)
            {
                string navId = NavigationService.GetNavigationVmId(MovieSearchViewModel);
                NavigationService.SaveState(navId, MovieSearchViewModel);
                NavigationService.NavigateTo("MoviePage", selectedMovie);
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            MovieSearchViewModel.PrevPage();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MovieSearchViewModel.NextPage();
        }

    }
}
