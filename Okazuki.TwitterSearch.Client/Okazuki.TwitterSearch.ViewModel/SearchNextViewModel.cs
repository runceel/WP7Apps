using Microsoft.Practices.Prism.Commands;
using Codeplex.Reactive;

namespace Okazuki.TwitterSearch.ViewModel
{
    public class SearchNextViewModel : TweetViewModel
    {
        /// <summary>
        /// Gets the SearchNextCommand.
        /// </summary>
        public ReactiveCommand SearchNextCommand
        {
            get
            {
                return this._parent.SearchNextCommand;
            }
        }

        public SearchNextViewModel(SearchViewViewModel parent) : base(null)
        {
            this._parent = parent;
        }

        private SearchViewViewModel _parent;
    }
}
