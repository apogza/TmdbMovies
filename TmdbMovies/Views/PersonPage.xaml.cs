﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
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
    public sealed partial class PersonPage : Page
    {
        public PersonPageViewModel ViewModel { get; private set; }

        public PersonPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Person person)
            { 
                ViewModel = NavigationService.RestoreState<PersonPageViewModel>();
                bool refreshSearch = ViewModel.PersonId != person.TmdbId;

                ViewModel.PersonId = person.TmdbId;
                DataContext = ViewModel;

                if (refreshSearch)
                {
                    await ViewModel.ResetSearch();
                }

                SetFavButtonToolTip();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }

        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton) sender;
            
            if (toggleButton.IsChecked == true)
            {
                ViewModel.AddToFavorites();
            }
            else
            {
                ViewModel.RemoveFromFavorites();
            }

            SetFavButtonToolTip();
        }

        private void SetFavButtonToolTip()
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            string toolTip = resourceLoader.GetString("PersonPageAddToWatchListToolTip");
            
            if (ViewModel.Person != null && ViewModel.Person.IsFavorite)
            {
                toolTip = resourceLoader.GetString("PersonPageRemoveFromWatchlistToolTip");
            }

            ToolTipService.SetToolTip(FavButton, toolTip);
        }
    }
}
