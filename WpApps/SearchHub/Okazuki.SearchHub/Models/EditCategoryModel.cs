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
using System.Collections.ObjectModel;

namespace Okazuki.SearchHub.Models
{
    public class EditCategoryModel : NotificationObject
    {
        private ObservableCollection<CategoryItem> _Categories;
        public ObservableCollection<CategoryItem> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                this.SetProperty<ObservableCollection<CategoryItem>>(() => Categories, ref _Categories, value);
            }
        }


        private CategoryItem _EditTargetCategory;
        public CategoryItem EditTargetCategory
        {
            get
            {
                return _EditTargetCategory;
            }
            set
            {
                this.SetProperty<CategoryItem>(() => EditTargetCategory, ref _EditTargetCategory, value);
            }
        }

        public void Commit()
        {
            if (!this.Categories.Contains(this.EditTargetCategory))
            {
                this.Categories.Add(this.EditTargetCategory);
            }
        }
    }
}
