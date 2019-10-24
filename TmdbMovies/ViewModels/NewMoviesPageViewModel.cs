﻿using System;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class NewMoviesPageViewModel: BaseMovieSearchViewModel
    {
        private DateTimeOffset _begindDate;

        public DateTimeOffset BeginDate
        {
            get { return _begindDate; }
            set { SetProperty(ref _begindDate, value); }
        }

        private DateTimeOffset _endDate;

        public DateTimeOffset EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public NewMoviesPageViewModel()
            :base()
        {
            ResetSearch();
        }

        public override void ResetSearch()
        {
            BeginDate = DateTime.Today.AddMonths(-3);
            EndDate = DateTime.Today;
            CurrentPage = 1;

            base.ResetSearch();
        }

        protected override string GetSearchString()
        {
            return string.Format("discover/movie?primary_release_date.gte={0}&primary_release_date.lte={1}&page={2}&api_key={3}",
                BeginDate.Date.ToString(TmdbConstants.JsonDateFormat), EndDate.Date.ToString(TmdbConstants.JsonDateFormat), 
                CurrentPage, TmdbConstants.TmdbKey);
        }
    }
}
