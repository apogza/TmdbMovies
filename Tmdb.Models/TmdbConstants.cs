namespace TmdbMovies.Models
{
    public static class TmdbConstants
    {
        public static string ApiKeyFile { get; } = "Resources/api_key.txt";
        public static string BaseUri { get; } = "https://api.themoviedb.org/3/";
        public static string JsonDateFormat { get; } = "yyyy-MM-dd";
        public static string TmdbKey { get; set; }

        public static string DbFileName { get; } = "TmdbFavorites.db";
    }
}
