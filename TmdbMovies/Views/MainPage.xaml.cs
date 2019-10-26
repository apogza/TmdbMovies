using System;
using Windows.Devices.Input;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Web.UI.Interop;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TmdbMovies.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            InitNavigation();
        }

        private void InitNavigation()
        {
            NavigationService.ContentFrame = ContentFrame;
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;

            if (item?.Tag == null)
            {
                return;
            }

            NavigationService.NavigateTo(item.Tag.ToString());            
        }

        private void MoviesNav_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateTo(typeof(NewMoviesPage), true);
        }

        private void NavigationView_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void MainPage_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {
                PointerPoint ptrPt = e.GetCurrentPoint(this);

                if (ptrPt.Properties.IsXButton1Pressed && ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                }

                if (ptrPt.Properties.IsXButton2Pressed && ContentFrame.CanGoForward)
                {
                    ContentFrame.GoForward();
                }
            }
        }

        private void MainPage_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Left && e.KeyStatus.IsMenuKeyDown && ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }

            if (e.Key == VirtualKey.Right && e.KeyStatus.IsMenuKeyDown && this.ContentFrame.CanGoForward)
            {
                ContentFrame.GoForward();
            }
        }
    }
}
