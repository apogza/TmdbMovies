using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        public IEnumerable<Movie> Movies { get; private set; }

        public void ReadInfo()
        {
            Movies = FavoritesService.GetFavorites<Movie>();
        }
    }
}
