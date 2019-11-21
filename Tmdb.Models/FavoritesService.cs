using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TmdbMovies.Models
{
    public class FavoritesService
    {
        public void AddToFavorites<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                var collection = db.GetCollection<T>();
                collection.Insert(entity);
            }
        }

        public void RemoveFromFavorites<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                var collection = db.GetCollection<T>();
                collection.Delete(entity.TmdbId);
            }
        }

        public IEnumerable<T> GetFavorites<T>() where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                var collection = db.GetCollection<T>();
                return collection.FindAll();
            }
        }

        public bool IsFavorite<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                var collection = db.GetCollection<T>();
                T result = collection.FindOne(Query.EQ("_id", entity.TmdbId));

                return result != null;
            }
        }

        private string GetDbFilePath()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var folderPath = localFolder.Path;
            var filePath = System.IO.Path.Combine(folderPath, TmdbConstants.DbFileName);

            return filePath;
        }
    }
}
