namespace Okazuki.TwitterSearch.Test.ViewModel
{
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
    using Okazuki.TwitterSearch.ViewModel;
    using Okazuki.TwitterSearch.Model;

    [TestClass]
    public class EditQueryViewModelTest : SilverlightTest
    {
        private SearchQuery query;
        private EditQueryViewModel target;

        [TestInitialize]
        public void SetUp()
        {
            this.query = new SearchQuery { Title = "Title", Query = "Query" };
            this.target = new EditQueryViewModel(query);
        }

        [TestCleanup]
        public void TearDown()
        {
            this.target = null;
            this.query = null;
        }

        [TestMethod]
        public void TestInvalidValue()
        {
            this.target.Title.ForceValidate();
            this.target.Query.ForceValidate();
            this.target.CommitCommand.CanExecute().Is(true);
            this.target.Query.Value = string.Empty;
            this.target.Query.ForceValidate();
            this.target.CommitCommand.CanExecute().Is(false);
        }

        [TestMethod]
        public void TestEdit()
        {
            var raisedCount = default(int);
            this.target.NavigateRequest.Raised += (s, e) =>
            {
                raisedCount++;
            };

            this.query.Title.Is("Title");
            this.query.Query.Is("Query");

            this.target.Title.Value = "EditValue";
            this.query.Title.Is("Title");

            this.target.CommitCommand.CanExecute().Is(true);
            this.target.CommitCommand.Execute();

            this.query.Title.Is("EditValue");
            raisedCount.Is(1);
        }
    }
}
