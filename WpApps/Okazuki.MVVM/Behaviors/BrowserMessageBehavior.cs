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
using Okazuki.MVVM.Messages;
using Microsoft.Phone.Tasks;

namespace Okazuki.MVVM.Behaviors
{
    public class BrowserMessageBehavior : MessageBehaviorBase<DependencyObject, BrowserMessage>
    {
        protected override void Invoke(BrowserMessage message)
        {
            var task = new WebBrowserTask();
            task.Uri = message.Content;
            task.Show();
        }
    }
}
