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
using System.Linq;
using Okazuki.MVVM.Commons;
using System.Collections.ObjectModel;

namespace Okazuki.SearchHub.ViewModels
{
    public class CategoryViewModel : ModelViewModel<MainPageViewModel, CategoryItem>
    {
        private IDisposable disposable;
        private ObservableCollection<FavViewModel> _Favorites = new ObservableCollection<FavViewModel>();
        public ObservableCollection<FavViewModel> Favorites
        {
            get
            {
                return _Favorites;
            }
            set
            {
                this.SetProperty<ObservableCollection<FavViewModel>>(() => Favorites, ref _Favorites, value);
            }
        }

        public CategoryViewModel(MainPageViewModel parent, CategoryItem model) : base(parent, model)
        {
            this.disposable = model.Favorites
                .Sync(this.Favorites, f => new FavViewModel(this, f));
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.disposable.Dispose();
        }
    }
}
