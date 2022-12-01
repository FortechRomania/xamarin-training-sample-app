using System;
using ObjCRuntime;
using TrainingPreparation.ViewModels;

namespace TrainingPreparation.iOS;

public partial class MovieTableViewCell : UITableViewCell
{
    protected MovieTableViewCell(NativeHandle handle) : base(handle)
    {
        // Note: this .ctor should not contain any initialization logic.
    }

    public void Bind(MovieViewModel viewModel)
    {
        MovieTitleLabel.Text = viewModel.Title;
        ImdbIdLabel.Text = viewModel.ImdbId;
    }
}
