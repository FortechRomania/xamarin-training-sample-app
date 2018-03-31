using Android.Support.V7.Widget;

namespace TrainingPreparation.Droid
{
    public partial class MoviesActivity
    {
        private RecyclerView _moviesRecyclerView;

        public RecyclerView MoviesRecyclerView => _moviesRecyclerView ?? (_moviesRecyclerView = FindViewById<RecyclerView>(Resource.Id.moviesRecyclerView));
    }
}
