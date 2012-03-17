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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Okazuki.MVVM.Commons;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Okazuki.MVVM.Test.Commons
{
    [TestClass]
    public class NotifyCollectionChangedExtensionsTest
    {
        private ObservableCollection<string> target;

        [TestInitialize]
        public void SetUp()
        {
            this.target = new ObservableCollection<string>();
        }

        [TestCleanup]
        public void TearDown()
        {
            this.target = null;
        }

        [TestMethod]
        public void CollectionChangedTest()
        {
            var args = default(NotifyCollectionChangedEventArgs);
            this.target.CollectionChangedAsObservable()
                .Subscribe(e => args = e);

            target.Add("aaa");
            args.Action.Is(NotifyCollectionChangedAction.Add);
            args.NewItems[0].Is("aaa");
            args.NewStartingIndex.Is(0);

            target[0] = "bbb";
            args.NewItems[0].Is("bbb");
            args.Action.Is(NotifyCollectionChangedAction.Replace);

            target.Clear();
            args.Action.Is(NotifyCollectionChangedAction.Reset);

            target.Add("aaa");
            target.Remove("aaa");
            args.Action.Is(NotifyCollectionChangedAction.Remove);
            args.OldItems[0].Is("aaa");
        }

        [TestMethod]
        public void AddTest()
        {
            var item = default(AddArgs<string>);
            target.CollectionAddAsObservable<string>()
                .Subscribe(i => item = i);

            target.Add("aaa");
            item.NewItems.Is("aaa");
            item.NewStartingIndex.Is(0);

            target.Clear();
            item.NewItems.Is("aaa");
            item.NewStartingIndex.Is(0);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var item = default(RemoveArgs<string>);
            target.CollectionRemoveAsObservable<string>()
                .Subscribe(i => item = i);

            target.Add("1");
            target.Add("2");
            target.Add("3");
            target.RemoveAt(1);

            item.Is(i => i.OldItems[0] == "2" && i.OldStartingIndex == 1);
        }

        [TestMethod]
        public void ResetTest()
        {
            var called = false;
            target.CollectionResetAsObservable()
                .Subscribe(_ => called = true);

            target.Add("1");
            target.Add("1");
            target.Add("1");
            target.Add("1");

            called.Is(false);
            target.Clear();
            called.Is(true);
        }

        [TestMethod]
        public void ReplaceTest()
        {
            var item = default(ReplaceArgs<string>);
            target.CollectionReplaceAsObservable<string>()
                .Subscribe(i => item = i);

            target.Add("1");
            target.Add("2");
            target.Add("3");

            target[1] = "a";
            item.Is(i => i.Index == 1 && i.NewItem == "a" && i.OldItem == "2");
        }
    }
}
