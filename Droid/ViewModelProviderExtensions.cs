using System;
using Android.Arch.Lifecycle;
using GalaSoft.MvvmLight;

namespace TrainingPreparation.Droid
{
    public static class ViewModelProvierExtensions
    {
        public static T Get<T>(this ViewModelProvider viewModelProvier, Func<T> viewModelFactory) where T : ViewModelBase
        {
            var androidViewModelHolder = (AndroidViewModelHolder)viewModelProvier.Get(Java.Lang.Class.FromType(typeof(AndroidViewModelHolder)));

            return androidViewModelHolder.GetViewModel(viewModelFactory);
        }

        /// Cannot make the class generic due to: Pending exception android.runtime.JavaProxyThrowable: System.NotSupportedException: Constructing instances of generic types from Java is not supported, as the type parameters cannot be determined.
        private class AndroidViewModelHolder : ViewModel
        {
            private object _viewModel;

            public T GetViewModel<T>(Func<T> viewModelFactory) where T : ViewModelBase
            {
                if (_viewModel == null)
                {
                    _viewModel = viewModelFactory();
                }

                return _viewModel as T;
            }
        }
    }
}
