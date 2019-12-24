using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class PeopleWatchlistViewModel: BaseViewModel
    {
        private IEnumerable<Person> _people;

        public IEnumerable<Person> People
        {
            get { return _people; }
            private set { SetProperty(ref _people, value); }
        }

        public async Task ReadInfo()
        {
            People = await DbService.GetEntities<Person>();
        }
    }
}
