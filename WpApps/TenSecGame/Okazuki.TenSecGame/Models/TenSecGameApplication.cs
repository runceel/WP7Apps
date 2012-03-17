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
using Okazuki.MVVM.Commons;

namespace Okazuki.TenSecGame.Models
{
    public class TenSecGameApplication : NotificationObject
    {
        public static TenSecGameApplication Context { get; private set; }

        static TenSecGameApplication()
        {
            Context = new TenSecGameApplication();
        }

        public TenSecGameApplication()
        {
            Context = this;
        }


    }
}
