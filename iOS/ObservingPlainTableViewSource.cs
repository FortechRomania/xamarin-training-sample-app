using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using UIKit;
using WeakEvent;

namespace TrainingPreparation.iOS
{
    public enum SelectionMode { None, Single }

    public class ObservingPlainTableViewSource<TElement> : UITableViewSource, INotifyPropertyChanged, WeakCollectionChangedEventHandler.IWrappedReference
    {
        private readonly WeakEventSource<ItemSelectedEventArgs> _weakEventSource = new WeakEventSource<ItemSelectedEventArgs>();

        private WeakReference<UITableView> _tableViewReference = new WeakReference<UITableView>(null);

        private WeakReference<IDelegate> _delegateReference = new WeakReference<IDelegate>(null);

        private ObservableCollection<TElement> _dataSource = new ObservableCollection<TElement>();

        private TElement _selectedItem;

        private WeakCollectionChangedEventHandler _weakCollectionChangedEventHandler;

        public ObservingPlainTableViewSource()
        {
            _weakCollectionChangedEventHandler = new WeakCollectionChangedEventHandler(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ItemSelectedEventArgs> WeakItemSelected
        {
            add => _weakEventSource.Subscribe(value);
            remove => _weakEventSource.Unsubscribe(value);
        }

        public interface IDelegate
        {
            string GetCellIdentifierForIndexPath(UITableView tableView, NSIndexPath indexPath) => null;

            void BindCell(UITableViewCell cell, TElement element);

            nfloat? GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => null;

            void OnScrolled(UIScrollView scrollView) { }

            void OnDraggingStarted(UIScrollView scrollView) { }
        }

        public ObservableCollection<TElement> DataSource
        {
            get => _dataSource;
            set
            {
                if (_dataSource != null)
                {
                    _weakCollectionChangedEventHandler.Unregister(_dataSource);
                }

                _dataSource = value;

                if (_dataSource != null)
                {
                    _weakCollectionChangedEventHandler.Register(_dataSource);
                }

                TableView?.ReloadData();
            }
        }

        public UITableViewRowAnimation AddOrRemoveAnimation { get; set; } = UITableViewRowAnimation.None;

        public UITableViewRowAnimation UpdateAnimation { get; set; } = UITableViewRowAnimation.None;

        public string CellReuseIdentifier { get; set; }

        public nfloat? EstimatedRowHeight { get; set; }

        public SelectionMode SelectionMode { get; set; } = SelectionMode.None;

        public IDelegate WeakDelegate
        {
            get => _delegateReference.TryGetTarget(out IDelegate target) ? target : null;
            set => _delegateReference = new WeakReference<IDelegate>(value);
        }

        public TElement SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value))
                {
                    return;
                }

                _selectedItem = value;
                NotifyPropertyChanged();

                if (value == null && TableView?.IndexPathForSelectedRow != null)
                {
                    TableView?.DeselectRow(TableView?.IndexPathForSelectedRow, true);
                }
                else if (DataSource.Contains(value))
                {
                    var index = NSIndexPath.FromRowSection(DataSource.IndexOf(value), 0);

                    if (index != TableView?.IndexPathForSelectedRow)
                    {
                        TableView?.SelectRow(index, true, UITableViewScrollPosition.Top);
                    }
                }
            }
        }

        private UITableView TableView => _tableViewReference.TryGetTarget(out UITableView tableView) ? tableView : null;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cellReuseIdentifier = WeakDelegate?.GetCellIdentifierForIndexPath(tableView, indexPath) ?? CellReuseIdentifier;

            var cell = tableView.DequeueReusableCell(cellReuseIdentifier);

            WeakDelegate?.BindCell(cell, DataSource[indexPath.Row]);

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            _tableViewReference = new WeakReference<UITableView>(tableview);

            return DataSource?.Count ?? 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var element = DataSource[indexPath.Row];

            if (SelectionMode == SelectionMode.None)
            {
                _weakEventSource.Raise(this, new ItemSelectedEventArgs(element));
                tableView.DeselectRow(indexPath, false);
            }
            else
            {
                SelectedItem = element;
            }
        }

        [Export("tableView:heightForRowAtIndexPath:")]
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return WeakDelegate?.GetHeightForRow(tableView, indexPath) ?? UITableView.AutomaticDimension;
        }

        [Export("tableView:estimatedHeightForRowAtIndexPath:")]
        public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return WeakDelegate?.GetHeightForRow(tableView, indexPath) ?? EstimatedRowHeight ?? UITableView.AutomaticDimension;
        }

        [Export("scrollViewDidScroll:")]
        public override void Scrolled(UIScrollView scrollView)
        {
            WeakDelegate?.OnScrolled(scrollView);
        }

        [Export("scrollViewWillBeginDragging:")]
        public override void DraggingStarted(UIScrollView scrollView)
        {
            WeakDelegate?.OnDraggingStarted(scrollView);
        }

        public void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var count = e.NewItems.Count;
                        var paths = new NSIndexPath[count];

                        for (var i = 0; i < count; i++)
                        {
                            paths[i] = NSIndexPath.FromRowSection(e.NewStartingIndex + i, 0);
                        }

                        TableView?.InsertRows(paths, AddOrRemoveAnimation);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        var count = e.OldItems.Count;
                        var paths = new NSIndexPath[count];

                        for (var i = 0; i < count; i++)
                        {
                            var index = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                            paths[i] = index;
                        }

                        TableView?.DeleteRows(paths, AddOrRemoveAnimation);
                    }

                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        var count = e.OldItems.Count;

                        for (var i = 0; i < count; i++)
                        {
                            var fromIndex = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                            var toIndex = NSIndexPath.FromRowSection(e.NewStartingIndex + i, 0);

                            TableView?.MoveRow(fromIndex, toIndex);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        if (AllItemsWereReplaced(e))
                        {
                            TableView?.ReloadSections(NSIndexSet.FromIndex(0), UpdateAnimation);
                        }
                        else
                        {
                            var count = e.OldItems.Count;
                            var paths = new NSIndexPath[count];

                            for (var i = 0; i < count; i++)
                            {
                                var index = NSIndexPath.FromRowSection(e.OldStartingIndex + i, 0);
                                paths[i] = index;
                            }

                            TableView?.ReloadRows(paths, UpdateAnimation);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        TableView?.ReloadData();
                    }

                    break;
            }
        }

        private bool AllItemsWereReplaced(NotifyCollectionChangedEventArgs e)
        {
            return e.Action == NotifyCollectionChangedAction.Replace && e.NewItems.Count == DataSource.Count;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class ItemSelectedEventArgs : EventArgs
        {
            public ItemSelectedEventArgs(TElement selectedItem)
            {
                SelectedItem = selectedItem;
            }

            public TElement SelectedItem { get; }
        }
    }
}
