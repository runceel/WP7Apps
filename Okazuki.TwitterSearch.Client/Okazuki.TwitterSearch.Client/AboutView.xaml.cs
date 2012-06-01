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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Marketplace;

namespace Okazuki.TwitterSearch.Client
{
    public partial class AboutView : UserControl
    {
        private static LicenseInformation licenseInfo = new LicenseInformation();

        public AboutView()
        {
            InitializeComponent();
            if (licenseInfo.IsTrial())
            {
                this.textBlock2.Text = "ついさち(評価版) : 機能制限, 使用期限無し";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var task = new MarketplaceDetailTask
            {
                ContentIdentifier = "ce8cbd60-f7b5-4a1b-afa4-8f91e38ddbf0",
                ContentType = MarketplaceContentType.Applications
            };
            task.Show();
        }
    }
}
