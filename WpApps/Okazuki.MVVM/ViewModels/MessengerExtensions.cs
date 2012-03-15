﻿namespace Okazuki.MVVM.ViewModels
{
    using System;
    using GalaSoft.MvvmLight.Messaging;
    using System.Reactive.Subjects;
    using System.Reactive.Linq;

    public static class MessengerExtensions
    {
        public static void SendWithEmptyToken<TMessage>(this Messenger self, TMessage message)
        {
            self.Send<TMessage>(message, Guid.Empty);
        }

        public static void SendWithViewModelToken<TMessage, TViewModel>(this Messenger self, TViewModel viewModel, TMessage message)
            where TViewModel : OkazukiViewModelBase
        {
            self.Send<TMessage>(message, viewModel.MessageToken);
        }

        public static IObservable<TMessage> SendWithEmptyAsObservable<TMessage>(
            this Messenger self, 
            Func<Action<TMessage>, 
            TMessage> factory)
        {
            var asyncSubject = new AsyncSubject<TMessage>();
            var message = factory(m =>
            {
                asyncSubject.OnNext(m);
                asyncSubject.OnCompleted();
            });

            self.SendWithEmptyToken<TMessage>(message);
            return asyncSubject.AsObservable();
        }

        public static IObservable<TMessage> SendWithViewModelTokenAsObservable<TMessage, TViewModel>(
            this Messenger self, 
            TViewModel viewModel, 
            Func<Action<TMessage>, TMessage> factory)
            where TViewModel : OkazukiViewModelBase
        {
            var asyncSubject = new AsyncSubject<TMessage>();
            var message = factory(m =>
            {
                asyncSubject.OnNext(m);
                asyncSubject.OnCompleted();
            });

            self.Send<TMessage>(message, viewModel.MessageToken);
            return asyncSubject.AsObservable();
        }
    }
}