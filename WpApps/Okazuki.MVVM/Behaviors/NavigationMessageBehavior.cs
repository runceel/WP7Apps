namespace Okazuki.MVVM.Behaviors
{
    using Microsoft.Phone.Controls;
    using Okazuki.MVVM.Messages;

    public class NavigationMessageBehavior : MessageBehaviorBase<PhoneApplicationPage, NavigationMessage>
    {
        protected override void Invoke(NavigationMessage message)
        {
            this.AssociatedObject.NavigationService.Navigate(message.Content);
        }
    }
}
