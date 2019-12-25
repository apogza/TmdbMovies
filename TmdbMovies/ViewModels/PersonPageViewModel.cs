using System;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class PersonPageViewModel: BaseMovieSearchViewModel
    {
        public int PersonId { get; set; }

        private Person _person;
        public Person Person
        {
            get { return this._person; }
            set { SetProperty(ref _person, value); }
        }

        public async Task ReadInfo()
        {
            string query = $"person/{PersonId}?api_key={TmdbConstants.TmdbKey}";
            Person result = await RestClient.GetEntity<Person>(query);
            result.IsFavorite = await IsFavorite(result);

            Person = result;
        }

        public void Reset()
        {
            Person = null;
            Movies = null;
        }

        public void AddToFavorites()
        {
            AddToFavorites(Person);
        }

        public void RemoveFromFavorites()
        {
            RemoveFromFavorites(Person);
        }

        public override async Task ResetSearch()
        {
            await base.ResetSearch();
            Reset();
            try
            {
                await ReadInfo();
                await Search(true);
            }
            catch(InvalidOperationException)
            {
                await DialogService.ShowErrorMessageDialog("ErrorLabel", "NetworkOrApiError");
            }
        }

        protected override string GetSearchString()
        {
            return $"discover/movie?with_people={PersonId}&page={CurrentPage}&api_key={TmdbConstants.TmdbKey}";
        }
    }
}
