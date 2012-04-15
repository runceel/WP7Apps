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
using System.Collections.ObjectModel;
using Okazuki.SearchHub.Models;
using Okazuki.MVVM.Commons;
using Okazuki.MVVM.ViewModels;
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;

namespace Okazuki.SearchHub.ViewModels
{
    public class CategoryListViewModel : PageViewModelBase
    {
        public RelayCommand AddCategoryCommand { get; private set; }

        private ObservableCollection<CategoryListCategoryViewModel> _Categories = new ObservableCollection<CategoryListCategoryViewModel>();
        public ObservableCollection<CategoryListCategoryViewModel> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                this.SetProperty<ObservableCollection<CategoryListCategoryViewModel>>(() => Categories, ref _Categories, value);
            }
        }

        public CategoryListViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            var d = this.Application
                .Categories
                .Sync(this.Categories, c => new CategoryListCategoryViewModel(this, c));
            this.Disposable.Add(d);

            this.AddCategoryCommand = new RelayCommand(() =>
            {
                this.Application.StartAddCategory();
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage("/Views/CategoryEditPage.xaml"));
            });
        }
    }
}
