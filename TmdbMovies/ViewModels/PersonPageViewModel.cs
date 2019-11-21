using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class PersonPageViewModel: BaseMovieSearchViewModel
    {

        private FavoritesService _favoriteService;

        public int PersonId { get; set; }

        public PersonPageViewModel()
        {
            _favoriteService = new FavoritesService();
        }

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
            result.IsFavorite = _favoriteService.IsFavorite(result);

            Person = result;
        }

        public void Reset()
        {
            Person = null;
            Movies = null;
        }

        public void AddToFavorites()
        {
            _favoriteService.AddToFavorites(Person);
            Person.IsFavorite = true;
        }

        public void RemoveFromFavorites()
        {
            _favoriteService.RemoveFromFavorites(Person);
            Person.IsFavorite = false;
        }

        protected override string GetSearchString()
        {
            return $"discover/movie?with_people={PersonId}&page={CurrentPage}&api_key={TmdbConstants.TmdbKey}";
        }
    }
}
