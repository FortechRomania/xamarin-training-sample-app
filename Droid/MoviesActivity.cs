using Android.App;
using Android.Arch.Lifecycle;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using TrainingPreparation.Services;
using TrainingPreparation.ViewModels;

namespace TrainingPreparation.Droid
{
    [Activity(Label = "MoviesActivity")]
    public partial class MoviesActivity : FragmentActivity
    {
        private MoviesViewModel _viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Movies);

            _viewModel = ViewModelProviders.Of(this).Get(() => new MoviesViewModel(new MovieSearchService()));

            MoviesRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            MoviesRecyclerView.SetAdapter(new ObservingRecyclerViewAdapter<MovieViewModel, MovieCellViewHolder>
            {
                Items = _viewModel.Movies,
                BindViewHolderDelegate = BindMovieCellViewHolder,
                CellLayoutId = Resource.Layout.MovieCell
            });

            _viewModel.ViewDidLoad();
        }

        private void BindMovieCellViewHolder(MovieCellViewHolder viewHolder, MovieViewModel viewModel, int position)
        {
            viewHolder.Bind(viewModel);
        }
    }
}
