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
using Microsoft.Silverlight.Testing;
using Okazuki.TwitterSearch.Model;
using System.Reactive.Linq;
using System.Linq;
using System.IO;

namespace Okazuki.TwitterSearch.Test.Model
{
    [TestClass]
    public class SearchQueryTest : SilverlightTest
    {
        private SearchQuery target;

        [TestInitialize]
        public void SeUp()
        {
            this.target = new SearchQuery();
        }

        [TestCleanup]
        public void TearDown()
        {
            this.target = null;
        }

        [TestMethod]
        [Asynchronous]
        public void SearchTest()
        {
            this.target.Query = "たろう";
            this.target.ResponseCount = 20;
            var result = this.target.Search();
            var completed = false;
            result.Subscribe(
                (tweet) =>
                {
                }, () =>
                {
                    completed = true;
                });

            this.EnqueueConditional(() => completed);
            this.EnqueueCallback(() =>
            {
                var results = result.ToEnumerable().ToList();
                Assert.AreEqual(20, results.Count);
            });
            this.EnqueueTestComplete();
        }

        [TestMethod]
        public void SaveTest()
        {
            var s = new MemoryStream();
            var qs = Enumerable.Range(1, 5).Select(i => new SearchQuery { Title = "aaa", Query = "bbb" });
            SearchQuery.Save(qs, s);
            s.Seek(0, SeekOrigin.Begin);
            var r = SearchQuery.Load(s);
            Assert.AreEqual(5, r.Count());
            Assert.AreEqual("aaa", r.First().Title);
            Assert.AreEqual("bbb", r.First().Query);
        }

    }
}
