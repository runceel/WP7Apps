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
using GalaSoft.MvvmLight.Messaging;
using System.Reactive.Linq;

namespace Okazuki.SearchHub.ViewModels
{
    public class CategoryListCategoryViewModel : ModelViewModel<CategoryListViewModel, CategoryItem>
    {
        public RelayCommand OpenBroserCommand { get; private set; }
        public RelayCommand UpCommand { get; private set; }
        public RelayCommand DownCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
       
        public CategoryListCategoryViewModel(CategoryListViewModel parent, CategoryItem model)
            : base(parent, model)
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            var application = SearchHubApplication.Current;

            this.UpCommand = new RelayCommand(() =>
            {
                application.Categories.Up(this.Model);
            });

            this.DownCommand = new RelayCommand(() =>
            {
                application.Categories.Down(this.Model);
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
                        application.Categories.Remove(this.Model);
                    });
            });
        }
    }
}
