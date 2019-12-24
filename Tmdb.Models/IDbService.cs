using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    public interface IDbService
    {
        Task AddEntity<T>(T entity) where T : BaseModel;
        Task RemoveEntity<T>(T entity) where T : BaseModel;
        Task<IEnumerable<T>> GetEntities<T>() where T : BaseModel;
        Task<bool> EntityExists<T>(T entity) where T : BaseModel;
    }
}
