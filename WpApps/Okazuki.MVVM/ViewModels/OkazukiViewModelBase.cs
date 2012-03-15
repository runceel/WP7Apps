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

namespace Okazuki.MVVM.ViewModels
{
    public class OkazukiViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        public Guid MessageToken { get; private set; }

        public OkazukiViewModelBase() : base(Messenger.Default)
        {
            this.MessageToken = Guid.NewGuid();
        }
    }
}
