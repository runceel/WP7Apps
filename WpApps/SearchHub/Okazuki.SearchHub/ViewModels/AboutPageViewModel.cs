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
using Microsoft.Phone.Marketplace;

namespace Okazuki.SearchHub.ViewModels
{
    public class AboutPageViewModel : PageViewModelBase
    {
        private static LicenseInformation licenseInfo = new LicenseInformation();

        public bool IsTrial
        {
            get
            {
                return licenseInfo.IsTrial();
            }
        }
    }
}
