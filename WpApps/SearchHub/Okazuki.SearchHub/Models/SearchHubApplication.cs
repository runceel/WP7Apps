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
using System.Linq;
using Okazuki.MVVM.Commons;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;

namespace Okazuki.SearchHub.Models
{
    public class SearchHubApplication : NotificationObject
    {
        public static SearchHubApplication Current { get; private set; }

        private ObservableCollection<CategoryItem> _Categories = new ObservableCollection<CategoryItem>();
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

        private string _Name = "お気に入り帳";
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                this.SetProperty<string>(() => Name, ref _Name, value);
            }
        }

        private CategoryItem _CurrentCategory;
        public CategoryItem CurrentCategory
        {
            get
            {
                return _CurrentCategory;
            }
            set
            {
                this.SetProperty<CategoryItem>(() => CurrentCategory, ref _CurrentCategory, value);
            }
        }


        static SearchHubApplication()
        {
            Current = new SearchHubApplication();
        }

        public SearchHubApplication()
        {
        }

        public void AddFavoriteToCurrentCategory(FavItem fav)
        {
            this.CurrentCategory.Favorites.Add(fav);
        }

        public void RemoveCategory(string category)
        {
            var targetCategory = this.Categories.FirstOrDefault(c => c.Name == category);
            if (targetCategory == null)
            {
                return;
            }

            this.Categories.Remove(targetCategory);
        }

        public void UpCategory(string category)
        {
            var targetCategory = this.Categories.FirstOrDefault(c => c.Name == category);
            if (targetCategory == null)
            {
                return;
            }

            this.Categories.Up(targetCategory);
        }

        public void DownCategory(string category)
        {
            var targetCategory = this.Categories.FirstOrDefault(c => c.Name == category);
            if (targetCategory == null)
            {
                return;
            }

            this.Categories.Down(targetCategory);
        }

        public void Save()
        {
            var f = IsolatedStorageFile.GetUserStoreForApplication();
            using (var stream = f.OpenFile("data.dat", FileMode.Create))
            {
                var serializer = new DataContractSerializer(typeof(ObservableCollection<CategoryItem>));
                serializer.WriteObject(stream, this.Categories);
            }
        }

        public void Load()
        {
            var f = IsolatedStorageFile.GetUserStoreForApplication();
            if (!f.FileExists("data.dat"))
            {
                this.Categories.Add(new CategoryItem
                {
                    Name = "お気に入り",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "Google", Url = "http://www.google.co.jp" },
                        new FavItem { Title = "Yahoo", Url = "http://www.yahoo.co.jp/" },
                        new FavItem { Title = "楽天", Url = "http://www.rakuten.co.jp/" },
                        new FavItem { Title = "かずきのBlog@hatena", Url = "http://d.hatena.ne.jp/okazuki" },
                        new FavItem { Title = "アプリケーションコード", Url = "https://github.com/runceel/WP7Apps" }
                    }
                });
                this.Categories.Add(new CategoryItem
                {
                    Name = "SNS",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "Facebook", Url = "http://www.facebook.com/" },
                        new FavItem { Title = "Twitter", Url = "https://twitter.com/" }
                    }
                });
                this.Categories.Add(new CategoryItem
                {
                    Name = "Sample",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "Facebook", Url = "http://www.facebook.com/" },
                        new FavItem { Title = "Twitter", Url = "https://twitter.com/" }
                    }
                });
                return;
            }

            using (var stream = f.OpenFile("data.dat", FileMode.Open))
            {
                var serializer = new DataContractSerializer(typeof(ObservableCollection<CategoryItem>));
                this.Categories.Clear();
                foreach (var c in serializer.ReadObject(stream) as ObservableCollection<CategoryItem>)
                {
                    this.Categories.Add(c);
                }
            }
        }
    }
}
