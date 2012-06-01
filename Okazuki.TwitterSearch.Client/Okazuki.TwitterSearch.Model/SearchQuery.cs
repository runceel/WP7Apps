namespace Okazuki.TwitterSearch.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Microsoft.Practices.Prism.ViewModel;
    using TweetSharp;

    public class SearchQuery : NotificationObject
    {
        #region Titleプロパティ
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (Equals(_title, value)) return;
                _title = value;
                this.RaisePropertyChanged(() => Title);
            }
        }
        #endregion

        #region Queryプロパティ
        private string _query;
        public string Query
        {
            get { return _query; }
            set
            {
                if (Equals(_query, value)) return;
                _query = value;
                this.RaisePropertyChanged(() => Query);
            }
        }
        #endregion

        #region ResponseCountプロパティ
        private int _responseCount;
        public int ResponseCount
        {
            get { return _responseCount; }
            set
            {
                if (Equals(_responseCount, value)) return;
                _responseCount = value;
                this.RaisePropertyChanged(() => ResponseCount);
            }
        }
        #endregion

        #region CurrentPageプロパティ
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (Equals(_currentPage, value)) return;
                _currentPage = value;
                this.RaisePropertyChanged(() => CurrentPage);
            }
        }
        #endregion


        public IObservable<TwitterSearchStatus> Search()
        {
            var client = new TwitterSearchClient();
            this.CurrentPage = 1;
            return client.Search(this.Query, this.CurrentPage, this.ResponseCount);
        }

        public IObservable<TwitterSearchStatus> SearchNext()
        {
            var client = new TwitterSearchClient();
            return client.Search(this.Query, ++this.CurrentPage, this.ResponseCount);
        }

        public static IEnumerable<SearchQuery> Load(Stream stream)
        {
            var c = new DataContractSerializer(typeof(IEnumerable<SearchQuery>));
            return c.ReadObject(stream) as IEnumerable<SearchQuery>;
        }

        public static void Save(IEnumerable<SearchQuery> query, Stream stream)
        {
            var c = new DataContractSerializer(typeof(IEnumerable<SearchQuery>));
            c.WriteObject(stream, query);
        }
    }
}
