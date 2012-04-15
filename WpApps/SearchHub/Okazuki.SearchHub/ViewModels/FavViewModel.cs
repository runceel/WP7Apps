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
using Okazuki.SearchHub.Models;
using Okazuki.MVVM.ViewModels;
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;
using GalaSoft.MvvmLight.Messaging;
using System.Reactive.Linq;

namespace Okazuki.SearchHub.ViewModels
{
    public class FavViewModel : ModelViewModel<CategoryViewModel, FavItem>
    {
        public SearchHubApplication Application { get; private set; }
        public RelayCommand OpenBroserCommand { get; private set; }
        public RelayCommand UpCommand { get; private set; }
        public RelayCommand DownCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }

        public FavViewModel(CategoryViewModel parent, FavItem model) : base(parent, model)
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            this.Application = SearchHubApplication.Current;

            this.OpenBroserCommand = new RelayCommand(() =>
            {
                this.Messenger.SendWithViewModelToken(
                    this,
                    new BrowserMessage(this.Model.Url));
            });

            this.UpCommand = new RelayCommand(() =>
            {
                this.Parent.Model.UpFavorite(this.Model);
            });

            this.DownCommand = new RelayCommand(() =>
            {
                this.Parent.Model.DownFavorite(this.Model);
            });

            this.EditCommand = new RelayCommand(() =>
            {
                var category = this.Parent.Model;
                var fav = this.Model;
                this.Application.StartEditFavorite(category, fav);
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage("/Views/FavAddPage.xaml"));
            });


            this.RemoveCommand = new RelayCommand(() =>
            {
                this.Messenger.SendWithViewModelTokenAsObservable<DialogMessage, MessageBoxResult>(
                    this,
                    callback => new DialogMessage("削除してもよろしいですか", callback) 
                    {
                        Caption = "確認",
                        Button = MessageBoxButton.OKCancel 
                    })
                    .Where(r => r == MessageBoxResult.OK)
                    .Subscribe(_ =>
                    {
                        this.Parent.Model.Favorites.Remove(this.Model);
                    });
            });
        }


    }
}
