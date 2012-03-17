namespace Okazuki.MVVM.Commons
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Linq;

    public static class NotifyCollectionChangedExtensions
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CollectionChangedAsObservable(
            this INotifyCollectionChanged self)
        {
            return Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => (_, e) => h(e),
                h => self.CollectionChanged += h,
                h => self.CollectionChanged -= h);
        }

        public static IObservable<AddArgs<T>> CollectionAddAsObservable<T>(
            this INotifyCollectionChanged self)
        {
            return self.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Add)
                .Select(e => new AddArgs<T> 
                { 
                    NewItems = e.NewItems.Cast<T>().ToArray(), 
                    NewStartingIndex = e.NewStartingIndex 
                });
        }

        public static IObservable<RemoveArgs<T>> CollectionRemoveAsObservable<T>(
            this INotifyCollectionChanged self)
        {
            return self.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
                .Select(e => new RemoveArgs<T>
                {
                    OldItems = e.OldItems.Cast<T>().ToArray(),
                    OldStartingIndex = e.OldStartingIndex
                });
        }

        public static IObservable<Unit> CollectionResetAsObservable(
            this INotifyCollectionChanged self)
        {
            return self.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Reset)
                .Select(e => Unit.Default);
        }

        public static IObservable<ReplaceArgs<T>> CollectionReplaceAsObservable<T>(
            this INotifyCollectionChanged self)
        {
            return self.CollectionChangedAsObservable()
                .Where(e => e.Action == NotifyCollectionChangedAction.Replace)
                .Select(e => new ReplaceArgs<T>
                {
                    NewItem = e.NewItems.Cast<T>().FirstOrDefault(),
                    OldItem = e.OldItems.Cast<T>().FirstOrDefault(),
                    Index = e.NewStartingIndex
                });
        }
    }

    public struct AddArgs<T>
    {
        public T[] NewItems { get; set; }
        public int NewStartingIndex { get; set; }
    }

    public struct RemoveArgs<T>
    {
        public T[] OldItems { get; set; }
        public int OldStartingIndex { get; set; }
    }

    public struct ReplaceArgs<T>
    {
        public T NewItem { get; set; }
        public T OldItem { get; set; }
        public int Index { get; set; }
    }
}
