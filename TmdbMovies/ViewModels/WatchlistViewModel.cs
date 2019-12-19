using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class WatchlistViewModel: BaseViewModel
    {
        public IEnumerable<Person> People { get; private set; }

        public void ReadInfo()
        {
            People = FavoritesService.GetFavorites<Person>();
        }
    }
}
