using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    public interface IFavoritesService
    {
        void AddToFavorites<T>(T entity) where T : BaseModel;
        void RemoveFromFavorites<T>(T entity) where T : BaseModel;
        IEnumerable<T> GetFavorites<T>() where T : BaseModel;
        bool IsFavorite<T>(T entity) where T : BaseModel;
    }
}
