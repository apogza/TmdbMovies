using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class MoviesWatchlistViewModel : BaseViewModel
    {
        private IEnumerable<Movie> _originalMovieList;

        private IEnumerable<Movie> _movies;
        public IEnumerable<Movie> Movies 
        {
            get { return _movies; }
            private set { SetProperty(ref _movies, value); }
        }

        public string _title;

        public string Title
        {
            get { return _title; }
            set 
            {                 
                SetProperty(ref _title, value); 
                if (string.IsNullOrWhiteSpace(value))
                {
                    FilterByTitle();
                }
            }
        }

        public async Task ReadInfo()
        {
            _originalMovieList = await DbService.GetEntities<Movie>();
            FilterByTitle();
        }

        public void FilterByTitle()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                Movies = _originalMovieList;
            }
            else
            {                
                Movies = _originalMovieList.Where(movie => movie.Title.ToLower().Contains(Title.ToLower()));
            }
        }
    }
}
