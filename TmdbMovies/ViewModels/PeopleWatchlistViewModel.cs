using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public class PeopleWatchlistViewModel: BaseViewModel
    {
        private IEnumerable<Person> _originalPeopleList;
        private IEnumerable<Person> _people;

        public IEnumerable<Person> People
        {
            get { return _people; }
            private set { SetProperty(ref _people, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            { 
                SetProperty(ref _name, value); 
                if (string.IsNullOrWhiteSpace(value))
                {
                    FilterByName();
                }
            }
        }

        public void FilterByName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                People = _originalPeopleList;
            }
            else
            {
                People = _originalPeopleList.Where(person => person.Name.ToLower().Contains(Name.ToLower()));
            }                            
        }

        public async Task ReadInfo()
        {
            _originalPeopleList = await DbService.GetEntities<Person>();
            FilterByName();
        }
    }
}
