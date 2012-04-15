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
using Okazuki.SearchHub.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Okazuki.MVVM.Commons;
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;
using System.Windows.Data;
using System.ComponentModel;

namespace Okazuki.SearchHub.ViewModels
{
    public class MainPageViewModel : PageViewModelBase
    {
        public RelayCommand AddFavoriteCommand { get; private set; }

        private int _CurrentCategoryIndex;
        public int CurrentCategoryIndex
        {
            get
            {
                return _CurrentCategoryIndex;
            }
            set
            {
                this.SetProperty<int>(() => CurrentCategoryIndex, ref _CurrentCategoryIndex, value);
            }
        }

        private ObservableCollection<CategoryViewModel> _Categories = new ObservableCollection<CategoryViewModel>();
        public ObservableCollection<CategoryViewModel> Categories
        {
            get
            {
                return _Categories;
            }

            set
            {
                this.SetProperty<ObservableCollection<CategoryViewModel>>(() => Categories, ref _Categories, value);
            }
        }

        public MainPageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            this.BuildChilren();

            this.AddFavoriteCommand = new RelayCommand(() =>
            {
                var currentCategory = this.Categories[this.CurrentCategoryIndex];
                this.Application.StartAddFavorite(currentCategory.Model);
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage("/Views/FavAddPage.xaml"));
            });
        }

        private void BuildChilren()
        {
            this.Disposable.Add(
                this.Application.Categories
                    .Sync(this.Categories, c => new CategoryViewModel(this, c)));
        }
    }
}
