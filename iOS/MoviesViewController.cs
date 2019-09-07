using Foundation;
using System;
using TrainingPreparation.Services;
using TrainingPreparation.ViewModels;
using UIKit;

namespace TrainingPreparation.iOS
{
    public partial class MoviesViewController : UIViewController
    {
        private MoviesViewModel _viewModel;

        public MoviesViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _viewModel = new MoviesViewModel(new MovieSearchService());

            var cellIdentifer = nameof(MovieTableViewCell);
            MoviesTableView.RegisterNibForCellReuse(UINib.FromName(nameof(MovieTableViewCell), null), cellIdentifer);

            MoviesTableView.Source = new ObservingTableViewSource<MovieViewModel, MovieTableViewCell>
            {
                Items = _viewModel.Movies,
                BindCellDelegate = BindMovieCell,
                CellIdentifier = cellIdentifer
            };

            _viewModel.ViewDidLoad();
        }

        private void BindMovieCell(MovieViewModel movieViewModel, MovieTableViewCell cell, NSIndexPath indexPath)
        {
            cell.Bind(movieViewModel);
        }
    }
}