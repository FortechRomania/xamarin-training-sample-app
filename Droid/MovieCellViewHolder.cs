using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using TrainingPreparation.ViewModels;

namespace TrainingPreparation.Droid;

public class MovieCellViewHolder : RecyclerView.ViewHolder
{
    private TextView _titleLabel;
    private TextView _imdbLabel;

    public MovieCellViewHolder(View itemView) : base(itemView)
    {
        _titleLabel = itemView.FindViewById<TextView>(Resource.Id.titleLabel);
        _imdbLabel = itemView.FindViewById<TextView>(Resource.Id.imdbId);
    }

    public void Bind(MovieViewModel viewModel)
    {
        _titleLabel.Text = viewModel.Title;
        _imdbLabel.Text = viewModel.ImdbId;
    }
}
