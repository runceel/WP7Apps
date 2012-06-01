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
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Phone.Tasks;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class ShareAction : TriggerAction<FrameworkElement>
    {

        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            if (e == null)
            {
                return;
            }

            var n = e.Context;
            
            var task = new ShareStatusTask
            {
                Status = n.Content as string
            };
            task.Show();
        }
    }
}
