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

namespace Okazuki.SearchHub.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
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
    }
}