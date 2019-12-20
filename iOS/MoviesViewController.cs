using System;
using TrainingPreparation.Services;
using TrainingPreparation.ViewModels;
using UIKit;

namespace TrainingPreparation.iOS
{
    public partial class MoviesViewController : UIViewController, ObservingPlainTableViewSource<MovieViewModel>.IDelegate
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

            MoviesTableView.Source = new ObservingPlainTableViewSource<MovieViewModel>
            {
                DataSource = _viewModel.Movies,
                WeakDelegate = this,
                CellReuseIdentifier = cellIdentifer
            };

            _viewModel.ViewDidLoad();
        }

        public void BindCell(UITableViewCell cell, MovieViewModel element)
        {
            (cell as MovieTableViewCell).Bind(element);
        }
    }
}