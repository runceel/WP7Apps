using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using Okazuki.MVVM.Commons;

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

        private string _Name = "ぶくさち";
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

        private EditFavoriteModel _EditFavoriteModel;
        public EditFavoriteModel EditFavoriteModel
        {
            get
            {
                return _EditFavoriteModel;
            }
            set
            {
                this.SetProperty<EditFavoriteModel>(() => EditFavoriteModel, ref _EditFavoriteModel, value);
            }
        }

        private EditCategoryModel _EditCategoryModel;
        public EditCategoryModel EditCategoryModel
        {
            get
            {
                return _EditCategoryModel;
            }
            set
            {
                this.SetProperty<EditCategoryModel>(() => EditCategoryModel, ref _EditCategoryModel, value);
            }
        }


        static SearchHubApplication()
        {
            Current = new SearchHubApplication();
        }

        public void StartAddFavorite(CategoryItem category)
        {
            this.StartEditFavorite(category, new FavItem());
        }

        public void StartEditFavorite(CategoryItem category, FavItem favItem)
        {
            this.EditFavoriteModel = new EditFavoriteModel
            {
                TargetCategory = category,
                EditTargetFavItem = favItem
            };
        }

        public void CommitAddFavorite()
        {
            if (this.EditFavoriteModel == null)
            {
                return;
            }

            this.EditFavoriteModel.Commit();
            this.EditFavoriteModel = null;
        }

        public void CancelAddFavorite()
        {
            this.EditFavoriteModel = null;
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
                // initial data
                this.Categories.Add(new CategoryItem
                {
                    Name = "良く見る",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "Google", Url = "http://www.google.co.jp" },
                        new FavItem { Title = "Yahoo", Url = "http://www.yahoo.co.jp/" },
                        new FavItem { Title = "Bing", Url = "http://www.bing.com/?cc=jp" },
                        new FavItem { Title = "楽天", Url = "http://www.rakuten.co.jp/" },
                        new FavItem { Title = "MSNニュース", Url = "http://sankei.jp.msn.com/" },
                        new FavItem { Title = "Google News", Url = "http://news.google.co.jp/?ned=jp" },
                   }
                });
                this.Categories.Add(new CategoryItem
                {
                    Name = "SNS",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "Facebook", Url = "http://www.facebook.com/" },
                        new FavItem { Title = "Twitter", Url = "https://twitter.com/" },
                        new FavItem { Title = "Google+", Url = "http://www.google.com/intl/ja/+/learnmore/" },
                        new FavItem { Title = "mixi", Url = "http://mixi.jp/" },
                        new FavItem { Title = "はてぶ", Url = "http://b.hatena.ne.jp/" },
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

        public void StartAddCategory()
        {
            this.StartEditCategory(new CategoryItem());
        }

        public void StartEditCategory(CategoryItem categoryItem)
        {
            this.EditCategoryModel = new EditCategoryModel
            {
                EditTargetCategory = categoryItem,
                Categories = this.Categories
            };
        }

        public void CommitEditCategory()
        {
            if (this.EditCategoryModel == null)
            {
                return;
            }

            this.EditCategoryModel.Commit();
            this.EditCategoryModel = null;
        }

        public void CancelEditCategory()
        {
            this.EditCategoryModel = null;
        }
    }
}
