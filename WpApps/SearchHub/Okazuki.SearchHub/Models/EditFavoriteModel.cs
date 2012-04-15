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

namespace Okazuki.SearchHub.Models
{
    public class EditFavoriteModel : NotificationObject
    {
        private CategoryItem _TargetCategory;
        public CategoryItem TargetCategory
        {
            get
            {
                return _TargetCategory;
            }
            set
            {
                this.SetProperty<CategoryItem>(() => TargetCategory, ref _TargetCategory, value);
            }
        }

        private FavItem _EditTargetFavItem;
        public FavItem EditTargetFavItem
        {
            get
            {
                return _EditTargetFavItem;
            }
            set
            {
                this.SetProperty<FavItem>(() => EditTargetFavItem, ref _EditTargetFavItem, value);
            }
        }

        public void Commit()
        {
            if (!this.TargetCategory.Favorites.Contains(this.EditTargetFavItem))
            {
                this.TargetCategory.Favorites.Add(this.EditTargetFavItem);
            }
        }
    }
}
