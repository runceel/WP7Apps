using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Linq;

namespace Okazuki.MVVM.Commons
{
    public static class ObservableCollectionUtil
    {
        public static IDisposable Sync<TFrom, TTo>(this ObservableCollection<TFrom> self, ObservableCollection<TTo> to, Func<TFrom, TTo> converter)
        {
            to.Clear();
            foreach (var item in self)
            {
                to.Add(converter(item));
            }
            return new CompositeDisposable(
                self.CollectionAddAsObservable<TFrom>()
                    .Subscribe(args =>
                    {
                        var index = args.NewStartingIndex;
                        foreach (var item in args.NewItems)
                        {
                            to.Insert(index++, converter(item));
                        }
                    }),
                self.CollectionRemoveAsObservable<TFrom>()
                    .Select(args => Enumerable.Range(args.OldStartingIndex, args.OldItems.Length))
                    .Subscribe(indexes =>
                    {
                        foreach (var i in indexes.OrderByDescending(i => i))
                        {
                            to.RemoveAt(i);
                        }
                    }),
                self.CollectionResetAsObservable()
                    .Subscribe(_ =>
                    {
                        to.Clear();
                        foreach (var item in self)
                        {
                            to.Add(converter(item));
                        }
                    }),
                self.CollectionReplaceAsObservable<TFrom>()
                    .Subscribe(args =>
                        to[args.Index] = converter(args.NewItem))
            );
        }
    }
}
