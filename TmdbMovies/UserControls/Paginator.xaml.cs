using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class Paginator : UserControl, INotifyPropertyChanged
    {

        public static DependencyProperty HasResultsDependencyProperty = DependencyProperty.Register("HasResults", typeof(bool), 
            typeof(Paginator), new PropertyMetadata(default(bool)));

        public bool HasResults
        {
            get { return (bool) GetValue(HasResultsDependencyProperty); }
            set 
            { 
                SetValue(HasResultsDependencyProperty, value); 
                RaisePropertyChanged();
            }
        }

        public static DependencyProperty IsSearchingDependencyProperty = DependencyProperty.Register("IsSearching", typeof(bool),
            typeof(Paginator), new PropertyMetadata(default(bool)));

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingDependencyProperty); }
            set
            {
                SetValue(IsSearchingDependencyProperty, value);
                RaisePropertyChanged();
            }
        }

        public static DependencyProperty CurrentPageDependencyProperty = DependencyProperty.Register("CurrentPage", typeof(int),
            typeof(Paginator), new PropertyMetadata(1));

        public int CurrentPage
        {
            get { return (int) GetValue(Paginator.CurrentPageDependencyProperty); }
            set
            {
                if (value != CurrentPage)
                {
                    SetValue(Paginator.CurrentPageDependencyProperty, value);
                    RaisePropertyChanged();
                }
            }
        }

        public static DependencyProperty TotalPagesDependencyProperty = DependencyProperty.Register("TotalPages", typeof(int),
            typeof(Paginator), new PropertyMetadata(default(int)));


        public int TotalPages
        {
            get { return (int) GetValue(Paginator.TotalPagesDependencyProperty); }
            set
            {
                SetValue(Paginator.TotalPagesDependencyProperty, value);
                RaisePropertyChanged();
            }
        }

        public event EventHandler<int> OnPageChange;

        public Paginator()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage += 1;
                OnPageChange?.Invoke(sender, CurrentPage);
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage -= 1;
                OnPageChange?.Invoke(sender, CurrentPage);
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void UIElement_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                if (sender as TextBox == null)
                {
                    return;
                }

                string newValue = new string((sender as TextBox).Text.Where(char.IsDigit).ToArray());
                (sender as TextBox).Text = string.IsNullOrWhiteSpace(newValue) ? "1" : newValue;

                int newPage = string.IsNullOrWhiteSpace(newValue) ? 1 : Convert.ToInt32(newValue);
                CurrentPage = newPage >= 1 && newPage <= TotalPages ? newPage : 1;

                OnPageChange?.Invoke(sender, CurrentPage);
            }
        }
    }
}