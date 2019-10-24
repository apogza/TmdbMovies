using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    [DataContract]
    public class SearchResults<T> where T: BaseModel
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "total_results")]
        public int TotalResults { get; set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "results")]
        public List<T> Results { get; set; }
    }
}
