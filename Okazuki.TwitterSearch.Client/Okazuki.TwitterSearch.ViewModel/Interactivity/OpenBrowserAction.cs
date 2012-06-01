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
using Microsoft.Phone.Tasks;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class OpenBrowserNotification : Notification
    {
        public Uri Uri { get; set; }
    }

    public class OpenBrowserAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            if (e == null)
            {
                return;
            }

            var n = e.Context as OpenBrowserNotification;
            if (n == null)
            {
                return;
            }

            var task = new WebBrowserTask
            {
                Uri = n.Uri
            };
            task.Show();
        }
    }
}
