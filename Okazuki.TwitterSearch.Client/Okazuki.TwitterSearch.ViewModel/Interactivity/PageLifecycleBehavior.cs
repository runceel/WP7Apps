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
using Microsoft.Phone.Controls;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class PageLifecycleBehavior : Behavior<PhoneApplicationPage>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += new RoutedEventHandler(AssociatedObject_Loaded);
            base.OnAttached();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded;
            this.AssociatedObject.NavigationService.Navigating += this.NavigationService_Navigating;
            this.AssociatedObject.NavigationService.Navigated += this.NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var page = this.AssociatedObject.DataContext as IPageLifecycle;
            if (page == null)
            {
                return;
            }

            page.NavigateTo();
        }

        private void NavigationService_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            var page = this.AssociatedObject.DataContext as IPageLifecycle;
            if (page == null)
            {
                return;
            }

            page.NavigateFrom(e.NavigationMode);
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.NavigationService.Navigating -= this.NavigationService_Navigating;
            this.AssociatedObject.NavigationService.Navigated -= this.NavigationService_Navigated;
            base.OnDetaching();
        }
    }
}
