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
using Okazuki.SearchHub.Models;
using Okazuki.MVVM.ViewModels;
using Okazuki.MVVM.Messages;
using Okazuki.MVVM.Commons;

namespace Okazuki.SearchHub.ViewModels
{
    public class CategoryAddPageViewModel : PageViewModelBase
    {
        public RelayCommand CommitCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                this.SetProperty<string>(() => Name, ref _Name, value);
            }
        }

        public CategoryAddPageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            var application = SearchHubApplication.Current;
            this.Name = application.EditCategoryModel.EditTargetCategory.Name;

            this.CommitCommand = new RelayCommand(() =>
            {
                application.EditCategoryModel.EditTargetCategory.Name = this.Name;
                application.CommitEditCategory();
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage(NavigationBehavior.Back));
            },
            () => !string.IsNullOrWhiteSpace(this.Name));

            this.CancelCommand = new RelayCommand(() =>
            {
                application.CancelEditCategory();
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage(NavigationBehavior.Back));
            });

            this.PropertyChangedAsObservable()
                .Subscribe(_ => this.CommitCommand.RaiseCanExecuteChanged());
        }


    }
}
