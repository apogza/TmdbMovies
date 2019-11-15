using TmdbMovies.Models;

namespace TmdbMovies.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        protected RestClient RestClient { get; }

        public BaseViewModel()
            : base()
        {
            RestClient = new RestClient(TmdbConstants.BaseUri);
        }
    }
}
