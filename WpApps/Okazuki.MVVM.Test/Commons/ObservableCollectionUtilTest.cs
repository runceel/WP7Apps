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
using System.Linq;

namespace Okazuki.MVVM.Test.Commons
{
    [TestClass]
    public class ObservableCollectionUtilTest
    {
        private ObservableCollection<Holder> source;
        private ObservableCollection<Holder2> target;
        private IDisposable disposable;

        [TestInitialize]
        public void SetUp()
        {
            this.source = new ObservableCollection<Holder>
            {
                new Holder { Value = 1 },
                new Holder { Value = 2 },
                new Holder { Value = 3 },
            };
            this.target = new ObservableCollection<Holder2>();
            this.disposable = this.source
                .Sync(target, h => new Holder2 { Value = h.Value * 2 });
        }

        [TestCleanup]
        public void TearDown()
        {
            this.disposable.Dispose();
            this.disposable = null;
            this.source = null;
            this.target = null;
        }

        [TestMethod]
        public void InitialStateTest()
        {
            this.source.Count.Is(3);
            this.target.Count.Is(3);
        }

        [TestMethod]
        public void SyncTest()
        {
            this.target
                .Select(h => h.Value)
                .Is(2, 4, 6);
        }

        [TestMethod]
        public void AddTest()
        {
            this.source.Add(new Holder { Value = 10 });
            this.target.Count.Is(4);
            this.target[3].Is(h => h.Value == 20);
        }

        [TestMethod]
        public void RemoveTest()
        {
            this.source.RemoveAt(1);
            this.target.Count.Is(2);
            this.target.Select(h => h.Value).Is(2, 6);
        }

        [TestMethod]
        public void ReplaceTest()
        {
            this.source[1] = new Holder { Value = 10 };
            this.target.Count.Is(3);
            this.target.Select(h => h.Value).Is(2, 20, 6);
        }

        [TestMethod]
        public void DisposeTest()
        {
            this.disposable.Dispose();
            this.source.Add(new Holder { Value = 1 });
            this.source.Count.Is(4);
            this.target.Count.Is(3);
        }

        [TestMethod]
        public void ClearTest()
        {
            this.source.Clear();
            this.source.Count.Is(0);
            this.target.Count.Is(0);
        }

        class Holder
        {
            public int Value { get; set; }
        }

        class Holder2
        {
            public int Value { get; set; }
        }
    }

}
