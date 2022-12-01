using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace TrainingPreparation.Droid;

public class ObservingRecyclerViewAdapter<TItem, THolder> : RecyclerView.Adapter where THolder : RecyclerView.ViewHolder
{
    private ObservableCollection<TItem> _items;

    public ObservableCollection<TItem> Items
    {
        get => _items;
        set
        {
            if (_items != null)
            {
                _items.CollectionChanged -= OnCollectionChanged;
            }

            _items = value;
            _items.CollectionChanged += OnCollectionChanged;
        }
    }

    public int CellLayoutId { get; set; }

    public Action<THolder, TItem, int> BindViewHolderDelegate { get; set; }

    public override int ItemCount 
    {
        get
        {
            return Items.Count;
        }
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        BindViewHolderDelegate?.Invoke((THolder)holder, Items[position], position);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var viewHolderType = typeof(THolder);

        var constructor = viewHolderType.GetConstructor(
            new[]
            {
                typeof(View)
            });

        if (constructor == null)
        {
            throw new InvalidOperationException(
                "No suitable constructor find for " + viewHolderType.FullName);
        }

        var view = LayoutInflater.From(parent.Context).Inflate(CellLayoutId, parent, false);
        var holder = constructor.Invoke(
            new object[]
            {
                view
            });

        return (RecyclerView.ViewHolder)holder;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        // You can check for the actual change and call a specific notify method
        NotifyDataSetChanged();
    }
}
