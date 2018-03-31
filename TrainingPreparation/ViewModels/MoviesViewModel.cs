using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TrainingPreparation.Services;

namespace TrainingPreparation.ViewModels
{
    public class MoviesViewModel : ViewModelBase
    {
        private IMovieSearchService _movieSearchService;

        public MoviesViewModel(IMovieSearchService movieSearchService)
        {
            _movieSearchService = movieSearchService;
        }

        public ObservableCollection<MovieViewModel> Movies { get; } = new ObservableCollection<MovieViewModel>();

        public async void ViewDidLoad()
        {
            try
            {
                var searchResponse = await _movieSearchService.SearchMovieAsync("Cars");

                Movies.Clear();
                searchResponse.Search.ForEach(movie => Movies.Add(new MovieViewModel { Title = movie.Title, ImdbId = movie.ImdbId }));
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }

        public string ImdbId { get; set; }
    }
}
