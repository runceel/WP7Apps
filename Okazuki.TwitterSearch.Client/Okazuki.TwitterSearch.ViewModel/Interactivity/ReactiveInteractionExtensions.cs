namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    using System;
    using Codeplex.Reactive;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using System.Reactive.Concurrency;

    public static class ReactiveInteractionExtensions
    {
        public static ReactiveInteractionRequest<T> ToReactiveInteractionRequest<T>(
            this IObservable<T> self, IScheduler scheduler = null)
            where T : Microsoft.Practices.Prism.Interactivity.InteractionRequest.Notification
        {
            return new ReactiveInteractionRequest<T>(
                self,
                scheduler ?? UIDispatcherScheduler.Default);
        }
    }
}
