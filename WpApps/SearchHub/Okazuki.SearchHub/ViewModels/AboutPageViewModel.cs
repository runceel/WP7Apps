namespace Okazuki.SearchHub.ViewModels
{
    using Microsoft.Phone.Marketplace;

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
