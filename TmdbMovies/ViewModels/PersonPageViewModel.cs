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
        public int PersonId { get; set; }

        public PersonPageViewModel()
        {
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
            Person = await RestClient.GetEntity<Person>(query);
        }

        public void Reset()
        {
            Person = null;
            Movies = null;
        }

        protected override string GetSearchString()
        {
            return $"discover/movie?with_people={PersonId}&page={CurrentPage}&api_key={TmdbConstants.TmdbKey}";
        }
    }
}
