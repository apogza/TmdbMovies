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
            set
            {
                if (CurrentPage != value && value > 0 && (TotalPages == 0 || value <= TotalPages))
                {
                    SetProperty(ref _currentPage, value);
                    OnChangePage();
                }
            }
        }

        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { SetProperty(ref _searchString, value); }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }

        private bool _hasResults;
        public bool HasResults
        {
            get { return _hasResults; }
            set { SetProperty(ref _hasResults, value); }
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

        public void PrevPage()
        {
            CurrentPage -= 1;
        }

        public void NextPage()
        {
            CurrentPage += 1;
        }

        protected virtual async void OnChangePage()
        {
            await Search(false);
        }

        public abstract Task Search(bool shouldRefreshCurrentPage);

        public virtual void ResetSearch()
        {
            HasResults = false;
        }

        protected abstract string GetSearchString();
    }
}
