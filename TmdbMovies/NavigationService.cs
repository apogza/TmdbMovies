using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using TmdbMovies.ViewModels;

namespace TmdbMovies
{
    public static class NavigationService
    {
        public static Frame ContentFrame { get; set; }

        private static Dictionary<string, BaseViewModel> NavigationState { get; } = new Dictionary<string, BaseViewModel>();

        public static void NavigateTo(string pageName, object parameter = null)
        {
            Type pageType = Type.GetType($"TmdbMovies.Views.{pageName}");

            if (pageType == null)
            {
                throw new ArgumentException("Unknown page requested");
            }

            ContentFrame?.NavigateToType(pageType, parameter, null);
        }

        public static void SaveState(string viewModelId, BaseViewModel viewModel)
        {
            NavigationState[viewModelId] = viewModel;
        }

        public static void DeleteState(string viewModelId)
        {
            if (NavigationState.ContainsKey(viewModelId))
            {
                NavigationState.Remove(viewModelId);
            }
        }

        public static T RestoreState<T>(string viewModelId) where T: BaseViewModel, new()
        {
            if (NavigationState.ContainsKey(viewModelId))
            {
                return NavigationState[viewModelId] as T;
            }

            return new T();
        }

        public static string GetNavigationVmId<T>() where T: BaseViewModel, new()
        {
            return typeof(T).Name;
        }

        public static string GetNavigationVmId(BaseViewModel vm)
        {
            return vm.GetType().Name;
        }

        public static void NavigateTo(Type pageType, object parameter = null)
        {
            ContentFrame?.NavigateToType(pageType, parameter, null);
        }
    }
}
