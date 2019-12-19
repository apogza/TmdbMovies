using TmdbMovies.Models;
using System.Collections.Generic;

namespace TmdbMovies.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        protected RestClient RestClient { get; }
        protected IFavoritesService FavoritesService { get; }

        public BaseViewModel()
            : base()
        {
            RestClient = new RestClient(TmdbConstants.BaseUri);
            FavoritesService = new FavoritesService();
        }

        protected void AddToFavorites<T>(T entity) where T: BaseModel
        {
            FavoritesService.AddToFavorites(entity);
            entity.IsFavorite = true;
        }

        protected void RemoveFromFavorites<T>(T entity) where T: BaseModel
        {
            FavoritesService.RemoveFromFavorites(entity);
            entity.IsFavorite = false;
        }

        protected bool IsFavorite<T>(T entity) where T: BaseModel
        {
            return FavoritesService.IsFavorite(entity);
        }

        protected IEnumerable<T> GetFavorites<T>() where T : BaseModel
        {
            return FavoritesService.GetFavorites<T>();
        }
    }
}
