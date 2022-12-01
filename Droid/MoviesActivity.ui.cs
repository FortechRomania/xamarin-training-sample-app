using AndroidX.RecyclerView.Widget;

namespace TrainingPreparation.Droid;

public partial class MoviesActivity
{
    private RecyclerView _moviesRecyclerView;

    public RecyclerView MoviesRecyclerView => _moviesRecyclerView ?? (_moviesRecyclerView = FindViewById<RecyclerView>(Resource.Id.moviesRecyclerView));
}
