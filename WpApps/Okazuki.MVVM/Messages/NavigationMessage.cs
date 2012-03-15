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
        public NavigationMessage(string uri): this(new Uri(uri))
        {
        }

        public NavigationMessage(Uri uri) : base(uri)
        {
        }
    }
}
