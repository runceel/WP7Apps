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
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Messaging;
using Okazuki.MVVM.Messages;
using Microsoft.Phone.Controls;

namespace Okazuki.MVVM.Behaviors
{
    public abstract class MessageBehaviorBase<T, TMessage> : Behavior<T>
        where T : DependencyObject
    {
        public Guid MessageToken
        {
            get { return (Guid)GetValue(MessageTokenProperty); }
            set { SetValue(MessageTokenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageToken.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageTokenProperty =
            DependencyProperty.Register(
                "MessageToken",
                typeof(Guid),
                typeof(MessageBehaviorBase<T, TMessage>),
                new PropertyMetadata(Guid.Empty, 
                    (s, e) => ((MessageBehaviorBase<T, TMessage>)s).OnMessageTokenChanged(e)));

        private void OnMessageTokenChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldToken = (Guid)e.OldValue;
            this.Unregister(oldToken);

            var newToken = (Guid)e.NewValue;
            this.Register(newToken);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Register(this.MessageToken);
        }

        protected override void OnDetaching()
        {
            this.Unregister(this.MessageToken);
            base.OnDetaching();
        }

        private void Register(Guid token)
        {
            Messenger.Default.Register<TMessage>(
                this.AssociatedObject,
                token,
                m => this.Invoke(m));
        }

        protected abstract void Invoke(TMessage message);

        private void Unregister(Guid token)
        {
            Messenger.Default.Unregister<TMessage>(
                this.AssociatedObject, 
                token);
        }
    }
}
