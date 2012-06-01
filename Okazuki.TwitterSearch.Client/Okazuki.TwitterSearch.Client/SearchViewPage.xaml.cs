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
using Okazuki.TwitterSearch.ViewModel;
using System.Diagnostics;

namespace Okazuki.TwitterSearch.Client
{
    public partial class SearchViewPage : PhoneApplicationPage
    {
        public SearchViewPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                this.DataContext = null;
            }
            base.OnNavigatedFrom(e);
        }
    }
}