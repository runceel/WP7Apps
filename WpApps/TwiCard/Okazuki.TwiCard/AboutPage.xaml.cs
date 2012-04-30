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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Marketplace;

namespace Okazuki_TwiCard
{
    public partial class AboutPage : PhoneApplicationPage
    {
        private static LicenseInformation licenseInfo = new LicenseInformation();

        public AboutPage()
        {
            InitializeComponent();
        }

        private void ShowMarketButton_Click(object sender, RoutedEventArgs e)
        {
            var task = new MarketplaceDetailTask();
            task.ContentIdentifier = "7d26a440-7f7d-4bd2-a9be-127a48daa6a3";
            task.Show();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockTrial.Visibility = licenseInfo.IsTrial() ? 
                Visibility.Visible : Visibility.Collapsed;
        }
    }
}
