using System;
using System.Collections.Generic;
using TmdbMovies.Models;
using System.Threading.Tasks;

namespace TmdbMovies.ViewModels
{
    public abstract class BaseMovieSearchViewModel: BaseSearchViewModel
    {
        private IEnumerable<Movie> _movies;
        public IEnumerable<Movie> Movies
        {
            get { return _movies; }
            set { SetProperty(ref _movies, value); }
        }

        public override async Task Search(bool shouldRefreshCurrentPage)
        {
            IsSearching = true;

            if (shouldRefreshCurrentPage)
            {
                CurrentPage = 1;
            }

            string query = GetSearchString();
            
            try
            {
                SearchResults<Movie> movieResults = await RestClient.GetSearchResults<Movie>(query);
                Movies = movieResults.Results;
                TotalPages = movieResults.TotalPages;
                HasResults = movieResults.TotalResults > 0;
            }
            finally
            {
                IsSearching = false;
            }

            return;
        }

        public override void ResetSearch()
        {
            base.ResetSearch();
            Movies = null;
        }
    }
}
