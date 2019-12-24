using TmdbMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TmdbMovies.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        protected RestClient RestClient { get; }
        protected IDbService DbService { get; }

        public BaseViewModel()
            : base()
        {
            RestClient = new RestClient(TmdbConstants.BaseUri);
            DbService = new DbService();
        }

        protected void AddToFavorites<T>(T entity) where T: BaseModel
        {
            DbService.AddEntity(entity);
            entity.IsFavorite = true;
        }

        protected void RemoveFromFavorites<T>(T entity) where T: BaseModel
        {
            DbService.RemoveEntity(entity);
            entity.IsFavorite = false;
        }

        protected async Task<bool> IsFavorite<T>(T entity) where T: BaseModel
        {
            return await DbService.EntityExists(entity);
        }

        protected async Task<IEnumerable<T>> GetFavorites<T>() where T : BaseModel
        {
            return await DbService.GetEntities<T>();
        }
    }
}
