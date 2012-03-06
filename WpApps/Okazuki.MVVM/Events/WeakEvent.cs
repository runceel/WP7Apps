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
using System.Reactive.Disposables;
using System.Reflection;
using System.Reactive.Linq;

namespace Okazuki.MVVM.Events
{
    public static class WeakEvent
    {
        public static IDisposable Handle<TEventHandler, TEventArgs>(
            Action<TEventHandler> addHandler,
            Action<TEventHandler> removeHandler,
            TEventHandler handler)
            where TEventArgs : EventArgs
            where TEventHandler : Delegate
        {
            Observable.FromEvent
            var h = handler as EventHandler;
            addHandler(handler);
            return Disposable.Create(() => removeHandler(handler));
        }

        public class WeakEventListener<TEventHandler, TEventArgs>
            where TEventArgs : EventArgs
        {
            public WeakReference Target { get; set; }
            public MethodInfo Method { get; set; }
            public Action<TEventHandler> RemoveHandler { get; set; }

            public void Invoke(object sender, TEventArgs e)
            {
                if (!this.Target.IsAlive)
                {
                    this.RemoveHandler(this.Invoke);
                    return;
                }

                this.Method.Invoke(
                    this.Target.Target,
                    new object[]
                    {
                        sender,
                        e
                    });
            }
        }
    }
}
