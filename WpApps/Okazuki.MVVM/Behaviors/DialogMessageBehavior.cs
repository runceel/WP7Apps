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
using GalaSoft.MvvmLight.Messaging;

namespace Okazuki.MVVM.Behaviors
{
    public class DialogMessageBehavior : MessageBehaviorBase<DependencyObject, DialogMessage>
    {
        protected override void Invoke(DialogMessage message)
        {
            var r = MessageBox.Show(
                message.Content ?? string.Empty,
                message.Caption ?? string.Empty,
                message.Button);
            if (message.Callback != null)
            {
                message.Callback(r);
            }
        }
    }
}
