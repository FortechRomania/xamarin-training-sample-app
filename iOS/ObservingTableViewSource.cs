using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Foundation;
using UIKit;

namespace TrainingPreparation.iOS
{
    public class ObservingTableViewSource<TItem, TCell> : UITableViewSource where TCell : UITableViewCell
    {
        private UITableView _tableView;
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

        public string CellIdentifier { get; set; }

        public Action<TItem, TCell, NSIndexPath> BindCellDelegate { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier);
            BindCellDelegate?.Invoke(Items[indexPath.Row], (TCell) cell, indexPath);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            _tableView = tableview;

            return Items.Count;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // You can check for the actual change and call a specific reload method
            _tableView.ReloadData();
        }
    }
}
