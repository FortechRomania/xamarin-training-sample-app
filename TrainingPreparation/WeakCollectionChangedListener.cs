using System;
using System.Collections.Specialized;

namespace TrainingPreparation
{
    public class WeakCollectionChangedEventHandler
    {
        private WeakReference<IWrappedReference> _wrappedReference = new WeakReference<IWrappedReference>(null);

        public WeakCollectionChangedEventHandler(IWrappedReference wrappedReference)
        {
            _wrappedReference = new WeakReference<IWrappedReference>(wrappedReference);
        }

        public interface IWrappedReference
        {
            void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        }

        private IWrappedReference WeakWrappedReference => _wrappedReference.TryGetTarget(out IWrappedReference target) ? target : null;

        public void Register(INotifyCollectionChanged notifyCollectionChanged)
        {
            notifyCollectionChanged.CollectionChanged += OnCollectionChanged;
        }

        public void Unregister(INotifyCollectionChanged notifyCollectionChanged)
        {
            notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WeakWrappedReference?.HandleCollectionChanged(sender, e);
        }
    }
}
