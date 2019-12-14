using System;
using System.Collections.Generic;
using TmdbMovies.Models;
using System.Threading.Tasks;
using TmdbMovies.Helpers;

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
            catch(InvalidOperationException)
            {
                await DialogService.ShowErrorMessageDialog("ErrorLabel", "NetworkOrApiError");
            }
            finally
            {
                IsSearching = false;
            }

            return;
        }

        public override Task ResetSearch()
        {
            base.ResetSearch();
            Movies = null;

            return Task.CompletedTask;
        }
    }
}
