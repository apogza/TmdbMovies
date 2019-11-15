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
                    ViewModel.Reset();

                    await ViewModel.ReadInfo();
                    await ViewModel.Search(true);
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }
    }
}
