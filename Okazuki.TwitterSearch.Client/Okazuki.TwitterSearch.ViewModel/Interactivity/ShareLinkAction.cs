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
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Windows.Interactivity;
using Microsoft.Phone.Tasks;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class ShareLinkAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            if (e == null)
            {
                return;
            }

            var n = e.Context as ShareLinkNotification;

            var task = new ShareLinkTask
            {
                Title = n.Title,
                Message = n.Content as string,
                LinkUri = n.Uri
            };
            task.Show();
        }
    }

    public class ShareLinkNotification : Notification
    {
        public Uri Uri { get; set; }
    }
}
