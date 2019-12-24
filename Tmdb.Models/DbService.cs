using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TmdbMovies.Models
{
    public class DbService : IDbService
    {
        public async Task AddEntity<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>();
                    collection.Insert(entity);
                });
            }
        }

        public async Task RemoveEntity<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>();
                    collection.Delete(entity.TmdbId);
                });
            }
        }

        public async Task<IEnumerable<T>> GetEntities<T>() where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                return await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>();
                    return collection.FindAll();
                });
            }
        }

        public async Task<bool> EntityExists<T>(T entity) where T : BaseModel
        {
            using (var db = new LiteDatabase(GetDbFilePath()))
            {
                T result = await Task.Run(() =>
                {
                    var collection = db.GetCollection<T>();
                    return collection.FindOne(Query.EQ("_id", entity.TmdbId));
                });

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
