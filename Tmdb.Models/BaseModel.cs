using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    [DataContract]
    public abstract class BaseModel
    {
        [DataMember(Name="id")]
        public int TmdbId { get; set; }
    }
}
