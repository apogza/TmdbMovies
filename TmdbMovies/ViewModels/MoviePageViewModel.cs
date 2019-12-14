using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using TmdbMovies.Helpers;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class MoviePageViewModel : BaseViewModel
    {
        private Movie _movie;
        
        public Movie Movie
        {
            get { return this._movie; }
            private set { SetProperty(ref this._movie, value); }
        }

        private IEnumerable<Person> FullCast { get; set; }
        private IEnumerable<Person> _cast;
        
        public IEnumerable<Person> Cast
        {
            get { return this._cast; }
            set { SetProperty(ref this._cast, value); }
        }


        private IEnumerable<Person> FullCrew { get; set; }
        private IEnumerable<Person> _crew;

        public IEnumerable<Person> Crew
        {
            get { return _crew; }
            set { SetProperty(ref _crew, value); }
        }


        private bool _isSearching;

        public bool IsSearching
        {
            get { return _isSearching; }
            set { SetProperty(ref _isSearching, value); }
        }

        public async Task ReadInfo(int movieId)
        {
            IsSearching = true;
            try
            {
                await ReadMovieInfo(movieId);
                await ReadMovieCast(movieId);
            }
            catch(InvalidOperationException)
            {
                await DialogService.ShowErrorMessageDialog("ErrorLabel", "NetworkOrApiError");
            }
            finally
            {
                IsSearching = false;
            }
        }

        private async Task ReadMovieInfo(int movieId)
        {
            string query = $"movie/{movieId}?api_key={TmdbConstants.TmdbKey}";
            Movie result = await RestClient.GetEntity<Movie>(query);
            result.IsFavorite = IsFavorite(result);
            Movie = result; 

            HasRevenue = Movie.Revenue > 0;
            HasBudget = Movie.Budget > 0;
        }

        private async Task ReadMovieCast(int movieId)
        {
            string query = $"movie/{movieId}/casts?api_key={TmdbConstants.TmdbKey}";
            CastResult castResult = await RestClient.GetResults<CastResult>(query);
            
            FullCast = castResult.Actors;
            FullCrew = castResult.Crew;

            Cast = FullCast.Where(c => !string.IsNullOrWhiteSpace(c.PictureUrl)).Take(10);
            HasMoreCast = Cast.Count() < FullCast.Count();

            Crew = FullCrew.Where(c => !string.IsNullOrWhiteSpace(c.PictureUrl)).Take(10);
            HasMoreCrew = Crew.Count() < FullCrew.Count();
        }

        public void RemoveFromFavorites()
        {
            RemoveFromFavorites(Movie);
        }

        public void AddToFavorites()
        {
            AddToFavorites(Movie);
        }

        private bool _hasMoreCast;
        public bool HasMoreCast
        {
            get { return _hasMoreCast; }
            set { SetProperty(ref _hasMoreCast, value); }
        }

        private bool _hasMoreCrew;
        public bool HasMoreCrew
        {
            get { return _hasMoreCrew; }
            set { SetProperty(ref _hasMoreCrew, value); }
        }

        public void ShowFullCast()
        {
            Cast = FullCast;
            HasMoreCast = false;
        }

        public void ShowFullCrew()
        {
            Crew = FullCrew;
            HasMoreCrew = false;
        }

        private bool _hasBudget;

        public bool HasBudget
        {
            get { return _hasBudget; }
            set { SetProperty(ref _hasBudget, value); }
        }

        private bool _hasRevenue;

        public bool HasRevenue
        {
            get { return _hasRevenue; }
            set { SetProperty(ref _hasRevenue, value); }
        }

        public void Reset()
        {
            Cast = null;
            Crew = null;
            Movie = null;
        }
    }
}