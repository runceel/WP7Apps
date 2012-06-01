namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;
    using System.Reactive;
    using Codeplex.Reactive;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using System.Reactive.Linq;

    public sealed class ReactiveInteractionRequest<T> : IInteractionRequest, IObservable<T>, IDisposable
        where T : Microsoft.Practices.Prism.Interactivity.InteractionRequest.Notification
    {
        private Subject<T> subject = new Subject<T>();
        private IScheduler scheduler;
        private IDisposable disposable;

        public event EventHandler<InteractionRequestedEventArgs> Raised;

        public ReactiveInteractionRequest(IObservable<T> source, IScheduler scheduler)
        {
            this.scheduler = scheduler;
            disposable = source.Subscribe(n => this.OnRaise(n));
        }

        public ReactiveInteractionRequest(IObservable<T> source) : this(source, UIDispatcherScheduler.Default)
        {
        }

        public ReactiveInteractionRequest() : this(Observable.Never<T>())
        {
        }

        private void OnRaise(T notification)
        {
            if (this.disposable == null)
            {
                throw new ObjectDisposedException("ReactiveInteractionRequest");
            }

            var h = this.Raised;
            if (h != null)
            {
                h(this, new InteractionRequestedEventArgs(notification, () =>
                    {
                        subject.OnNext(notification);
                    }));
            }
        }

        public void Raise(T notification)
        {
            this.OnRaise(notification);
        }

        public void RaiseOnScheduler(T notification)
        {
            this.scheduler.Schedule(() =>
                {
                    this.OnRaise(notification);
                });
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.subject.Subscribe(observer);
        }

        public void Dispose()
        {
            if (this.disposable == null)
            {
                return;
            }

            this.subject.OnCompleted();
            this.disposable.Dispose();
            this.disposable = null;
        }
    }
}
