// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TrainingPreparation.iOS
{
    [Register ("MovieTableViewCell")]
    partial class MovieTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ImdbIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MovieTitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImdbIdLabel != null) {
                ImdbIdLabel.Dispose ();
                ImdbIdLabel = null;
            }

            if (MovieTitleLabel != null) {
                MovieTitleLabel.Dispose ();
                MovieTitleLabel = null;
            }
        }
    }
}