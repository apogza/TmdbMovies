using System;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class TopRatedMoviesPageViewModel : BaseMovieSearchViewModel
    {
        private DateTimeOffset _beginDate;

        public DateTimeOffset BeginDate
        {
            get { return _beginDate; }
            set { SetProperty( ref _beginDate, value); }
        }

        public TopRatedMoviesPageViewModel()
            :base()
        {
            BeginDate = DateTime.Today.AddYears(-10);
        }

        protected override string GetSearchString()
        {
            return string.Format("discover/movie?sort_by=vote_average.desc&vote_count.gte=5000&api_key={0}&page={1}&primary_release_date.gte={2}", 
                TmdbConstants.TmdbKey, CurrentPage, BeginDate.Date.ToString(TmdbConstants.JsonDateFormat));
        }
    }
}
