using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class MoviesWatchlistViewModel : BaseViewModel
    {
        private IEnumerable<Movie> _movies;
        public IEnumerable<Movie> Movies 
        {
            get { return _movies; }
            private set { SetProperty(ref _movies, value); }
        }

        public async Task ReadInfo()
        {
            Movies = await DbService.GetEntities<Movie>();
        }
    }
}
