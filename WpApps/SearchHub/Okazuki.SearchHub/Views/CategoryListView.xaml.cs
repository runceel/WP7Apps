using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using Microsoft.Phone.Controls;

namespace Okazuki.SearchHub.Views
{
    public partial class CategoryListView : PhoneApplicationPage
    {
        public CategoryListView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                var vm = this.DataContext as ICleanup;
                vm.Cleanup();
            }
        }
    }
}