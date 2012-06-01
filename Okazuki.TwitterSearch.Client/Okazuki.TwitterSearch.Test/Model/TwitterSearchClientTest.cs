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
using TweetSharp;
using System.Collections.Generic;

namespace Okazuki.TwitterSearch.Test.Model
{
    [TestClass]
    public class TwitterSearchClientTest : SilverlightTest
    {
        [TestMethod]
        public void CreateInstanceTest()
        {
            var c = new TwitterSearchClient();
        }

        [TestMethod]
        [Asynchronous]
        public void SearchTest()
        {
            var c = new TwitterSearchClient();
            var ret = c.Search("okazuki", 1, 20);
            var completed = false;
            ret.Subscribe(status =>
            {
            },ex =>
            {
                Assert.Fail(ex.ToString());
            }, () =>
            {
                completed = true;
            });
            
            this.EnqueueConditional(() => completed);
            this.EnqueueCallback(() =>
            {
                var results = ret.ToEnumerable().ToList();
                Assert.IsNotNull(results);
                Assert.AreEqual(20, results.Count());
            });
            this.EnqueueTestComplete();
        }
    }
}
