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
    public sealed partial class SearchPeoplePage : Page
    {
        public SearchPeoplePageViewModel ViewModel { get; set; }

        public SearchPeoplePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = NavigationService.RestoreState<SearchPeoplePageViewModel>();
            DataContext = ViewModel;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            NavigationService.SaveState(ViewModel);
        }

        private async void SearchBox_OnQuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            await ViewModel.Search(true);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Person selectedPerson)
            {
                NavigationService.NavigateTo("PersonPage", selectedPerson);
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PrevPage();
        }

        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            string newValue = new string(sender.Text.Where(char.IsDigit).ToArray());
            sender.Text = string.IsNullOrWhiteSpace(newValue) ? "1" : newValue;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NextPage();
        }
    }
}
