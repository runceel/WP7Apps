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
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;
using Okazuki.MVVM.ViewModels;
using Okazuki.MVVM.Commons;
using Okazuki.SearchHub.Models;

namespace Okazuki.SearchHub.ViewModels
{
    public class FavAddPageViewModel : PageViewModelBase
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                this.SetProperty<string>(() => Title, ref _Title, value);
            }
        }

        private string _Url;
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                this.SetProperty<string>(() => Url, ref _Url, value);
            }
        }


        public FavAddPageViewModel()
        {
            var application = SearchHubApplication.Current;

            this.AddCommand = new RelayCommand(() =>
            {

            },
            () => !string.IsNullOrWhiteSpace(this.Title) && !string.IsNullOrWhiteSpace(this.Url));

            this.CancelCommand = new RelayCommand(() =>
            {
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage(NavigationBehavior.Back));
            });

            this.PropertyChangedAsObservable()
                .Subscribe(_ => this.AddCommand.RaiseCanExecuteChanged());
        }
    }
}
