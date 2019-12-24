using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using TmdbMovies.Models;
using Windows.ApplicationModel.Resources;

namespace TmdbMovies.ViewModels
{
    public class SearchPeoplePageViewModel : BaseSearchViewModel
    {
        private IEnumerable<Person> _actors;

        public IEnumerable<Person> People
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
                People = actorResults.Results;
                TotalPages = actorResults.TotalPages;
                HasResults = actorResults.TotalResults > 0;
            }
            catch (InvalidOperationException)
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();
                string errorMessage = resourceLoader.GetString("ApiError");
                string errorTitle = resourceLoader.GetString("ErrorLabel");

                await DialogService.ShowSimpleMessageDialog(errorTitle, errorMessage);
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
