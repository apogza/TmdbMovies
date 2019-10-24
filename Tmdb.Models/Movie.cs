using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    [DataContract]
    public class Movie : BaseModel
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        [DataMember(Name = "release_date")]
        public string ReleaseDateJson { get; set; }

        [DataMember(Name = "budget")]
        public decimal Budget { get; set; }

        [DataMember(Name = "revenue")]
        public decimal Revenue { get; set; }

        [DataMember(Name = "runtime")]
        public int? Duration { get; set; }

        [IgnoreDataMember]
        public string YearMonthOfRelease
        {
            get
            {
                return ReleaseDate != DateTime.MinValue ? 
                    $"({this.ReleaseDate.ToString("MMM yyyy")})"
                    : string.Empty;
            }
        }

        [IgnoreDataMember]
        public DateTime ReleaseDate
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ReleaseDateJson) ?
                    DateTime.ParseExact(ReleaseDateJson, TmdbConstants.JsonDateFormat, null)
                    : DateTime.MinValue;
            }
        }

        [IgnoreDataMember]
        public string ThumbnailPoster
        {
            get { return $"https://image.tmdb.org/t/p/w185{this.PosterUrl}"; }
        }

        public string FullPoster
        {
            get { return $"https://image.tmdb.org/t/p/w500{this.PosterUrl}"; }
        }

        [DataMember(Name = "poster_path")]
        public string PosterUrl { get; set; }

        [DataMember(Name = "popularity")]
        public float Popularity { get; set; }

        [DataMember(Name = "vote_average")]
        public float VoteAverage { get; set; }
    }
}
