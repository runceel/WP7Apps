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
using GalaSoft.MvvmLight.Messaging;

namespace Okazuki.MVVM.Messages
{
    public class NavigationMessage : GenericMessage<Uri>
    {
        public NavigationBehavior NavigationBehavior { get; private set; }

        public NavigationMessage(string uri): this(new Uri(uri, UriKind.Relative))
        {
        }

        public NavigationMessage(Uri uri) : base(uri)
        {
        }

        public NavigationMessage(NavigationBehavior behavior) : base(new Uri("/"))
        {
            this.NavigationBehavior = behavior;
        }
    }

    public enum NavigationBehavior
    {
        None,
        Back,
        Forward
    }
}
