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
using Microsoft.Phone.Controls;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class NavigateAction : TriggerAction<PhoneApplicationPage>
    {
        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            var n = e.Context as NavigateNotification;
            if (n.IsBack)
            {
                if (this.AssociatedObject.NavigationService.CanGoBack)
                {
                    this.AssociatedObject.NavigationService.GoBack();
                }

                return;
            }

            this.AssociatedObject.NavigationService.Navigate(n.NavigateUri);
        }
    }
}
