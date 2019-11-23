using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TmdbMovies.Models;
using TmdbMovies.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TmdbMovies.Views
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MoviePage : Page
    {
        public MoviePageViewModel ViewModel { get; private set; }

        public MoviePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Movie movie)
            {
                ViewModel = NavigationService.RestoreState<MoviePageViewModel>();
                DataContext = ViewModel;

                if (ViewModel.Movie == null || ViewModel.Movie.TmdbId != movie.TmdbId)
                {
                    ViewModel.Reset();
                    await ViewModel.ReadInfo(movie.TmdbId);
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Person selectedPerson)
            {
                NavigationService.NavigateTo("PersonPage", selectedPerson);
            }
        }

        private void ShowFullCast_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowFullCast();
        }

        private void ShowFullCrew_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowFullCrew();
        }

        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;

            if (toggleButton.IsChecked == true)
            {
                ViewModel.AddToFavorites();
            }
            else
            {
                ViewModel.RemoveFromFavorites();
            }
        }
    }
}
