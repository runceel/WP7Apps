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
using Okazuki.MVVM.ViewModels;
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;

namespace Okazuki.TenSecGame.ViewModels
{
    public class MainPageViewModel : OkazukiViewModelBase
    {
        public RelayCommand StartCommand { get; private set; }

        public MainPageViewModel()
        {
            this.StartCommand = new RelayCommand(() =>
            {
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage("/Views/GamePage.xaml"));
            });
        }
    }
}
