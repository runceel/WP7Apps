using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Okazuki.MVVM.Commons;
using Okazuki.SearchHub.ViewModels;
using Microsoft.Phone.Shell;

namespace Okazuki.SearchHub.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (PhoneApplicationService.Current.State.ContainsKey(Constraits.CurrentCategoryIndexKey))
            {
                this.pivotCategories.SelectedIndex = (int)PhoneApplicationService.Current.State[Constraits.CurrentCategoryIndexKey];
                PhoneApplicationService.Current.State.Remove(Constraits.CurrentCategoryIndexKey);
            }
        }

        private void ApplicationBarMenuItemShowCategoryList_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(
                new Uri("/Views/CategoryListView.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItemAbout_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(
                new Uri("/Views/AboutPage.xaml", UriKind.Relative));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;
            this.GetViewModel<MainPageViewModel>().CurrentCategoryIndex = pivot.SelectedIndex;
        }
    }
}