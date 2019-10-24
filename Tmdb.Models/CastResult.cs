using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    [DataContract]
    public class CastResult
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "cast")]
        public IEnumerable<Person> Actors { get; set; }

        [DataMember(Name = "crew")]
        public IEnumerable<Person> Crew { get; set; }
    }
}
