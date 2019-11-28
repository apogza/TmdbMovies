using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class SearchPeoplePageViewModel : BaseSearchViewModel
    {
        private IEnumerable<Person> _actors;

        public IEnumerable<Person> Actors
        {
            get { return _actors;}
            set { SetProperty(ref _actors, value); }
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
                SearchResults<Person> actorResults = await RestClient.GetSearchResults<Person>(query);
                Actors = actorResults.Results;
                TotalPages = actorResults.TotalPages;
                HasResults = actorResults.TotalResults > 0;
            }
            catch (InvalidOperationException)
            {
                await DialogService.ShowMessageDialog("Error", "An error has occurred while retrieving data. Please check your TMDB key.");
            }
            finally
            {
                IsSearching = false;
            }
        }

        protected override string GetSearchString()
        {
            return string.Format("search/person?query={0}&api_key={1}&page={2}",
                SearchString, TmdbConstants.TmdbKey, CurrentPage);
        }
    }
}
