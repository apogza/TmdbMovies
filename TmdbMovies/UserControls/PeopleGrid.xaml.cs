using System.Collections.Generic;
using TmdbMovies.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TmdbMovies.UserControls
{
    public sealed partial class PeopleGrid : UserControl
    {
        public static DependencyProperty PeopleProperty = 
            DependencyProperty.Register("People", typeof(IEnumerable<Person>), typeof(PeopleGrid), new PropertyMetadata(null));

        public IEnumerable<Person> People
        {
            get { return (IEnumerable<Person>)GetValue(PeopleGrid.PeopleProperty); }
            set { SetValue(PeopleGrid.PeopleProperty, value); }            
        }

        public static DependencyProperty IsSearchingProperty =
            DependencyProperty.Register("IsSearching", typeof(bool), typeof(PeopleGrid), new PropertyMetadata(false));

        public bool IsSearching
        {
            get { return (bool)GetValue(PeopleGrid.IsSearchingProperty); }
            set { SetValue(PeopleGrid.IsSearchingProperty, value); }
        }

        public PeopleGrid()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Person selectedPerson)
            {
                NavigationService.NavigateTo("PersonPage", selectedPerson);
            }
        }
    }
}
