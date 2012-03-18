namespace Okazuki.MVVM.Commons
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> self)
        {
            var list = self as List<T>;
            if (list != null)
            {
                return new ObservableCollection<T>(list);
            }

            return new ObservableCollection<T>(self);
        }
    }
}
