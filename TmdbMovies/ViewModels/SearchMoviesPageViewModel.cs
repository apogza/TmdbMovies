using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class SearchMoviesPageViewModel: BaseMovieSearchViewModel
    {
        protected override string GetSearchString()
        {
            return string.Format("search/movie?query={0}&api_key={1}&page={2}",
                    SearchString, TmdbConstants.TmdbKey, CurrentPage);
        }
    }
}
