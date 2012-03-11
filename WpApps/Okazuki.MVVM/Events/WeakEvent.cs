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
        #region EventHandler<T>
        public static IDisposable Handle<TEventArgs>(
            Action<EventHandler<TEventArgs>> addHandler,
            Action<EventHandler<TEventArgs>> removeHandler,
            EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            var l = new WeakEventListener<TEventArgs>(handler, removeHandler);
            addHandler(l.Invoke);
            return Disposable.Create(() => removeHandler(l.Invoke));
        }

        private class WeakEventListener<TEventArgs>
            where TEventArgs : EventArgs
        {
            private WeakReference Target { get; set; }
            private WeakReference Handler { get; set; }
            private Action<EventHandler<TEventArgs>> RemoveHandler { get; set; }

            public WeakEventListener(EventHandler<TEventArgs> eventHandler, Action<EventHandler<TEventArgs>> removeHandler)
            {
                this.Target = new WeakReference(eventHandler.Target);
                this.Handler = new WeakReference(eventHandler);
                this.RemoveHandler = removeHandler;
            }

            public void Invoke(object sender, TEventArgs e)
            {
                if (!this.Target.IsAlive)
                {
                    this.RemoveHandler(this.Invoke);
                    return;
                }

                if (!this.Handler.IsAlive)
                {
                    this.RemoveHandler(this.Invoke);
                    return;
                }

                var h = this.Handler.Target as EventHandler<TEventArgs>;
                h(sender, e);
            }
        }
        #endregion

        #region 任意のデリゲート
        public static IDisposable Handle<TEventHandler, TEventArgs>(
            Func<Action<TEventArgs>, TEventHandler> converter,
            Action<TEventHandler> addHandler,
            Action<TEventHandler> removeHandler,
            Action<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            var h = converter(handler);
            var l = new WeakEventHandlerListener<TEventHandler, TEventArgs>(
                h,
                addHandler,
                removeHandler,
                handler);

            return Disposable.Create(() => removeHandler(h));
        }

        private class WeakEventHandlerListener<TEventHandler, TEventArgs>
            where TEventArgs : EventArgs
        {
            private WeakReference weakHandler;
            private Action<TEventHandler> removeHandler;
            private TEventHandler invokeHandler;
            private Action<TEventArgs> action;

            public WeakEventHandlerListener(
                TEventHandler h,
                Action<TEventHandler> addHandler,
                Action<TEventHandler> removeHandler,
                Action<TEventArgs> handler)
            {
                this.weakHandler = new WeakReference(h);
                this.action = handler;
            }

            public void Invoke(TEventArgs e)
            {
                if (!this.weakHandler.IsAlive)
                {
                    this.removeHandler(invokeHandler);
                    return;
                }

                var h = this.weakHandler.Target as Action<TEventArgs>;
                h(e);
            }
        }


        #endregion

    }
}
