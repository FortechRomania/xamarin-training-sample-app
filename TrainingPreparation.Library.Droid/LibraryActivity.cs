using Android.App;
using Android.OS;

namespace TrainingPreparation.Library.Droid
{
    [Activity(Label = "LibraryActivity")]
    public class LibraryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Library);
        }
    }
}
