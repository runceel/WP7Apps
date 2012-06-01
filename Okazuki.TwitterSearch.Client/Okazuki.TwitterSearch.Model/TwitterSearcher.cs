namespace Okazuki.TwitterSearch.Model
{
    using System.Collections.ObjectModel;

    public class TwitterSearcher
    {
        private ObservableCollection<SearchQuery> _queries = new ObservableCollection<SearchQuery>();

        public ObservableCollection<SearchQuery> Queries
        {
            get { return this._queries; }
        }
    }
}
