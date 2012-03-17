namespace Okazuki.MVVM.Commons
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Linq;

    public static class NotifyPropertyChangedExtensions
    {
        public static IObservable<string> PropertyChangedAsObservable(this INotifyPropertyChanged self)
        {
            return Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => (_, e) => h(e),
                h => self.PropertyChanged += h,
                h => self.PropertyChanged -= h)
                .Select(args => args.PropertyName);
        }
    }
}
