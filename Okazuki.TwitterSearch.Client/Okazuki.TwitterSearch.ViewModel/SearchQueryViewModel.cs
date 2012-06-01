using System;
using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;
using System.Reactive.Linq;
using Okazuki.TwitterSearch.Model;
using Okazuki.TwitterSearch.ViewModel.Interactivity;

namespace Okazuki.TwitterSearch.ViewModel
{
    public class SearchQueryViewModel : ViewModelBase
    {
        public SearchQueryViewModel(MainPageViewModel parent, SearchQuery model)
        {
            this.Parent = new ReactiveProperty<MainPageViewModel>(parent);
            this.Model = new ReactiveProperty<SearchQuery>(model);

            this.Title = this.Model.Value
                .ObserveProperty(o => o.Title)
                .ToReactiveProperty(this.Model.Value.Title);

            this.Query = this.Model.Value
                .ObserveProperty(o => o.Query)
                .ToReactiveProperty(this.Model.Value.Query);

            this.SearchCommand = new ReactiveCommand();
            this.SearchCommand.Subscribe(_ =>
            {
                var vm = new SearchViewViewModel(this.Model.Value);
                vm.QueryCommand.Execute(null);
                this.Parent.Value.SelectedQuery.Value = vm;
                this.Parent.Value.NavigateRequest.Raise(
                    new NavigateNotification(new Uri("/SearchViewPage.xaml", UriKind.Relative)));
            });
        }

        public ReactiveProperty<MainPageViewModel> Parent { get; private set; }
        public ReactiveProperty<string> Title { get; private set; }
        public ReactiveProperty<string> Query { get; private set; }
        public ReactiveProperty<SearchQuery> Model { get; private set; }

        public ReactiveCommand SearchCommand { get; private set; }
    }
}
