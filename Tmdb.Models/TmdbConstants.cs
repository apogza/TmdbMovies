using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovies.Models
{
    public static class TmdbConstants
    {
        public static string BaseUri { get; } = "https://api.themoviedb.org/3/";
        public static string JsonDateFormat { get; } = "yyyy-MM-dd";
        public static string TmdbKey { get; set; }

        public static string DbFileName { get; } = "TmdbFavorites.db";
    }
}
