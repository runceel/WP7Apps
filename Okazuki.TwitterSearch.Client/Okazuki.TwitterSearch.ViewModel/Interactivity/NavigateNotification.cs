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
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    public class NavigateNotification : Notification
    {
        public Uri NavigateUri { get; private set; }

        public bool IsBack { get; private set; }

        public NavigateNotification(Uri navigateUri)
        {
            this.NavigateUri = navigateUri;
        }

        public NavigateNotification(bool isBack)
        {
            this.IsBack = isBack;
        }
    }
}
