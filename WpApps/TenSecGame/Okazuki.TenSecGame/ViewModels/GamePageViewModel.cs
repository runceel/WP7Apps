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
using Okazuki.TenSecGame.Models;

namespace Okazuki.TenSecGame.ViewModels
{
    public class GamePageViewModel : OkazukiViewModelBase
    {
        private TenSecGameApplication model;

        public RelayCommand CountDownStartCommand { get; private set; }

        public bool IsStarted
        {
            get
            {
                return this.model.Game.IsStarted;
            }
        }

        public GamePageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            model = TenSecGameApplication.Context;

            this.CountDownStartCommand = new RelayCommand(() =>
            {
                if (this.IsStarted)
                {
                    model.Game.Stop();
                }
                else
                {
                    model.Game.Start();
                }
                this.RaisePropertyChanged(() => IsStarted);
            });
        }


    }
}
