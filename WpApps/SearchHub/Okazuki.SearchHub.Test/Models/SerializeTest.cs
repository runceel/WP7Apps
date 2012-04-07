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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okazuki.SearchHub.Models;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace Okazuki.SearchHub.Test.Models
{
    [TestClass]
    public class SerializeTest
    {
        private SearchHubApplication target;

        [TestInitialize]
        public void SetUp()
        {
            this.target = new SearchHubApplication();
        }

        [TestCleanup]
        public void TearDown()
        {
            this.target = null;
            var fs = IsolatedStorageFile.GetUserStoreForApplication();
            if (fs.FileExists("data.dat"))
            {
                fs.DeleteFile("data.dat");
            }
        }

        [TestMethod]
        public void SaveAndLoadTest()
        {
            this.target.Categories = new ObservableCollection<CategoryItem>
            {
                new CategoryItem
                {
                    Name = "category1",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "fav1", Url = "url1" },
                        new FavItem { Title = "fav2", Url = "url2" },
                        new FavItem { Title = "fav3", Url = "url3" },
                    }
                },
                new CategoryItem
                {
                    Name = "category2",
                    Favorites = new ObservableCollection<FavItem>
                    {
                        new FavItem { Title = "fav1", Url = "url1" },
                        new FavItem { Title = "fav2", Url = "url2" },
                    }
                },
            };

            IsolatedStorageFile.GetUserStoreForApplication().FileExists("data.dat").Is(false);
            this.target.Save();
            IsolatedStorageFile.GetUserStoreForApplication().FileExists("data.dat").Is(true);

            var deserialize = new SearchHubApplication();
            deserialize.Load();
            deserialize.Categories.Count.Is(2);
            deserialize.Categories[0].Is(c => c.Name == "category1" && c.Favorites.Count == 3);
            deserialize.Categories[0].Favorites[0].Is(f => f.Title == "fav1" && f.Url == "url1");
            deserialize.Categories[1].Is(c => c.Name == "category2" && c.Favorites.Count == 2);
            deserialize.Categories[1].Favorites[0].Is(f => f.Title == "fav1" && f.Url == "url1");
        }

    }
}
