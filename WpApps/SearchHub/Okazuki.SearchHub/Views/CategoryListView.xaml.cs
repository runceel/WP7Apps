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
using GalaSoft.MvvmLight;
using System.Windows.Navigation;

namespace Okazuki.SearchHub.Views
{
    public partial class CategoryListView : PhoneApplicationPage
    {
        public CategoryListView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
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