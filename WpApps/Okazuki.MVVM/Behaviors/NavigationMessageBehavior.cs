namespace Okazuki.MVVM.Behaviors
{
    using Microsoft.Phone.Controls;
    using Okazuki.MVVM.Messages;
    using System;

    public class NavigationMessageBehavior : MessageBehaviorBase<PhoneApplicationPage, NavigationMessage>
    {
        protected override void Invoke(NavigationMessage message)
        {
            var navigatonService = this.AssociatedObject.NavigationService;
            if (message.NavigationBehavior == NavigationBehavior.None)
            {
                navigatonService.Navigate(message.Content);
                return;
            }

            if (message.NavigationBehavior == NavigationBehavior.Back)
            {
                if (navigatonService.CanGoBack)
                {
                    navigatonService.GoBack();
                }
                return;
            }

            if (message.NavigationBehavior == NavigationBehavior.Forward)
            {
                if (navigatonService.CanGoForward)
                {
                    navigatonService.GoForward();
                }
                return;
            }

            throw new ArgumentException("navigation behavior: " + message.NavigationBehavior);
        }
    }
}
