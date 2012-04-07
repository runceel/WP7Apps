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
using Okazuki.MVVM.Commons;
using System.Linq;
using System.Collections.ObjectModel;

namespace Okazuki.SearchHub.Models
{
    public class CategoryItem : NotificationObject
    {
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

        private ObservableCollection<FavItem> _Favorites = new ObservableCollection<FavItem>();
        public ObservableCollection<FavItem> Favorites
        {
            get
            {
                return _Favorites;
            }
            set
            {
                this.SetProperty<ObservableCollection<FavItem>>(() => Favorites, ref _Favorites, value);
            }
        }

        public void UpFavorite(FavItem fav)
        {
            this.Favorites.Up(fav);
        }

        public void DownFavorite(FavItem fav)
        {
            this.Favorites.Down(fav);
        }
    }
}
