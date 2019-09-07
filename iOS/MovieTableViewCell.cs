using System;
using TrainingPreparation.ViewModels;
using UIKit;

namespace TrainingPreparation.iOS
{
    public partial class MovieTableViewCell : UITableViewCell
    {
        protected MovieTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void Bind(MovieViewModel viewModel)
        {
            MovieTitleLabel.Text = viewModel.Title;
            ImdbIdLabel.Text = viewModel.ImdbId;
        }
    }
}
