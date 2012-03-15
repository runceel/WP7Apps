namespace Okazuki.MVVM.Events
{
    using System;
    using System.Reactive.Disposables;

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
    }
}
