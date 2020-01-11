using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.ViewModels
{
    public abstract class BaseSearchViewModel : BaseViewModel
    {
        protected int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        private string _searchString;
        public virtual string SearchString
        {
            get { return _searchString; }
            set { SetProperty(ref _searchString, value); }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set 
            { 
                SetProperty(ref _totalPages, value);
                RaisePropertyChanged(nameof(HasPages));
            }
        }

        private bool? _hasResults;
        public bool? HasResults
        {
            get { return _hasResults; }
            set { SetProperty(ref _hasResults, value); }
        }

        public bool HasPages
        {
            get { return TotalPages > 1; }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { SetProperty(ref _isSearching, value); }
        }

        public BaseSearchViewModel()
            : base()
        {
            _currentPage = 1;
        }

        public virtual async void OnPageChange(int currentPage = 1)
        {
            if (CurrentPage != currentPage)
            {
                CurrentPage = currentPage;
            }

            await Search(false);
        }

        public abstract Task Search(bool shouldRefreshCurrentPage);

        public virtual Task ResetSearch()
        {
            HasResults = null;
            return Task.CompletedTask;
        }

        protected abstract string GetSearchString();
    }
}
