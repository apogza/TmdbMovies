using System;
using System.Collections.Generic;
using TmdbMovies.Models;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using System.Linq;

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

        private bool _filterByPeopleWatchlist;
        public bool FilterByPeopleWatchlist
        {
            get { return _filterByPeopleWatchlist; }
            set { SetProperty(ref _filterByPeopleWatchlist, value); }
        }

        public override async Task Search(bool shouldRefreshCurrentPage)
        {
            IsSearching = true;

            if (shouldRefreshCurrentPage)
            {
                CurrentPage = 1;
            }

            string query = GetSearchString();

            if (FilterByPeopleWatchlist)
            {
                string peopleIdList = string.Join("|", await GetPeopleIdList());
                query = string.Format("{0}&with_people={1}", query, peopleIdList);
            }

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
            FilterByPeopleWatchlist = false;

            return Task.CompletedTask;
        }

        protected async Task<IEnumerable<int>> GetPeopleIdList()
        {
            IEnumerable<Person> people = await DbService.GetEntities<Person>();
            IEnumerable<int> peopleIdList = people.Select(person => person.TmdbId);

            return peopleIdList;
        }
    }
}
