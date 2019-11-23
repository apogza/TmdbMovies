using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    [DataContract]
    public class Person: BaseModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [BsonIgnore]
        [DataMember(Name = "character")]
        public string CharacterName { get; set; }

        [BsonIgnore]
        [DataMember(Name = "job")]
        public string Job { get;set; }

        [DataMember(Name = "profile_path")]
        public string PictureUrl { get; set; }

        [BsonIgnore]
        [DataMember(Name = "biography")]
        public string Biography { get; set; }

        [BsonIgnore]
        [DataMember(Name="birthday")]
        public string BirthdayJson { get; set; }

        [BsonIgnore]
        [IgnoreDataMember]
        public DateTime Birthday
        {
            get { return !string.IsNullOrWhiteSpace(BirthdayJson) ? 
                            DateTime.ParseExact(BirthdayJson, TmdbConstants.JsonDateFormat, null)
                            : DateTime.MinValue; }
        }

        [BsonIgnore]
        [DataMember(Name = "deathday")]
        public string DeathdayJson { get; set; }

        [BsonIgnore]
        [IgnoreDataMember]
        public DateTime Deathday
        {
            get { return !string.IsNullOrWhiteSpace(DeathdayJson) ? 
                            DateTime.ParseExact(DeathdayJson, TmdbConstants.JsonDateFormat, null)
                            :DateTime.MinValue;}
        }
        
        [BsonIgnore]
        [DataMember(Name = "place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [BsonIgnore]
        [IgnoreDataMember]
        public bool IsDead
        {
            get { return !string.IsNullOrWhiteSpace(DeathdayJson); }
        }

        [IgnoreDataMember]
        public string Thumbnail
        {
            get { return $"https://image.tmdb.org/t/p/w66_and_h66_face{PictureUrl}"; }
        }

        [IgnoreDataMember]
        public string Profile
        {
            get { return $"https://image.tmdb.org/t/p/original/{PictureUrl}"; }
        }
    }
}
