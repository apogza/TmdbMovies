using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    public class FavoritesService
    {
        public void AddToFavorites<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(TmdbConstants.DbFileName))
            {
                var collection = db.GetCollection<T>();
                collection.Insert(entity);
            }
        }

        public void RemoveFromFavorites<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(TmdbConstants.DbFileName))
            {
                var collection = db.GetCollection<Person>();
                collection.Delete(entity.TmdbId);
            }
        }

        public IEnumerable<T> GetFavorites<T>() where T : BaseModel
        {
            using (var db = new LiteDatabase(TmdbConstants.DbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.FindAll();
            }
        }
    }
}
