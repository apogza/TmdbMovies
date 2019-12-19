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
using TmdbMovies.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TmdbMovies.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoritesPage : Page
    {
        public FavoritesViewModel ViewModel { get; set; }

        public FavoritesPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = NavigationService.RestoreState<FavoritesViewModel>();
            ViewModel.ReadInfo();
        }

        private void MovieGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }
    }
}
