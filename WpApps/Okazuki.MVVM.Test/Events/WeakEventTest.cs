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
using Okazuki.MVVM.Events;

namespace Okazuki.MVVM.Test.Events
{
    [TestClass]
    public class WeakEventTest
    {
        [TestMethod]
        public void WeakEventHandleTest()
        {
            var source = new EventSource();
            var subscriber = new EventSubscriber();
            var weakSubscriber = new WeakReference(subscriber);

            var d = WeakEvent.Handle<EventArgs>(
                h => source.Event += h,
                h => source.Event -= h,
                subscriber.Handler);

            Assert.IsNotNull(source);
            Assert.IsNotNull(subscriber);
            Assert.AreEqual(0, subscriber.Count);

            source.Raise();
            Assert.AreEqual(1, subscriber.Count);

            subscriber = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(weakSubscriber.IsAlive);
        }

    }

    class EventSource
    {
        public event EventHandler<EventArgs> Event;
        public void Raise()
        {
            var h = this.Event;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }
    }

    class EventSubscriber
    {
        public int Count { get; private set; }
        public void Handler(object sender, EventArgs e)
        {
            this.Count++;
        }
    }
}
