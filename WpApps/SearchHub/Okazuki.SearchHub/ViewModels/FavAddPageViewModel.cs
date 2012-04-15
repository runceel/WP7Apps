using System;
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Commons;
using Okazuki.MVVM.Messages;
using Okazuki.MVVM.ViewModels;

namespace Okazuki.SearchHub.ViewModels
{
    public class FavAddPageViewModel : PageViewModelBase
    {
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }

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
            if (this.IsInDesignMode)
            {
                return;
            }

            this.Title = this.Application.EditFavoriteModel.EditTargetFavItem.Title;
            this.Url = this.Application.EditFavoriteModel.EditTargetFavItem.Url;

            this.AddCommand = new RelayCommand(() =>
            {
                if (this.Application.EditFavoriteModel != null)
                {
                    this.Application.EditFavoriteModel.EditTargetFavItem.Title = this.Title;
                    this.Application.EditFavoriteModel.EditTargetFavItem.Url = this.Url;
                    this.Application.CommitAddFavorite();
                }

                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage(NavigationBehavior.Back));
            },
            () =>
            {
                if (string.IsNullOrWhiteSpace(this.Title) || string.IsNullOrWhiteSpace(this.Url))
                {
                    return false;
                }

                var dummy = default(Uri);
                return Uri.TryCreate(this.Url, UriKind.Absolute, out dummy);
            });

            this.CancelCommand = new RelayCommand(() =>
            {
                this.Application.CancelAddFavorite();
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage(NavigationBehavior.Back));
            });

            this.PropertyChangedAsObservable()
                .Subscribe(_ => this.AddCommand.RaiseCanExecuteChanged());
        }
    }
}
