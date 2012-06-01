namespace Okazuki.TwitterSearch.ViewModel
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using Codeplex.Reactive;
    using Okazuki.TwitterSearch.Model;
    using Okazuki.TwitterSearch.ViewModel.Interactivity;

    public class CreateSearchQueryViewModel : PageViewModel
    {
        public CreateSearchQueryViewModel() : base("検索条件登録")
        {
        }

        public CreateSearchQueryViewModel(MainPageViewModel parent) : base("検索条件登録")
        {
            this.Title = new ReactiveProperty<string>()
                .SetValidateError(s => string.IsNullOrWhiteSpace(s) ? "タイトルを入力してください" : null);
            this.Query = new ReactiveProperty<string>()
                .SetValidateError(s => string.IsNullOrWhiteSpace(s) ? "クエリを入力してください" : null);
            this.Title.Value = "";
            this.Query.Value = "";

            this.CreateCommand = Observable.Merge(
                this.Title.Do(_ => this.Title.ForceValidate()).Select(_ => Unit.Default),
                this.Query.Do(_ => this.Query.ForceValidate()).Select(_ => Unit.Default))
                .Select(_ => string.IsNullOrWhiteSpace(this.Title.Error) && string.IsNullOrWhiteSpace(this.Query.Error))
                .ToReactiveCommand(false);
            this.CreateCommand.Subscribe(_ =>
            {
                parent.Queries.Add(
                    new SearchQueryViewModel(parent,
                        new SearchQuery { Title = this.Title.Value, Query = this.Query.Value }));
                this.NavigateRequest.Raise(
                    new NavigateNotification(true));
            });

            this.CancelCommand = new ReactiveCommand();
            this.CancelCommand.Subscribe(_ =>
            {
                this.NavigateRequest.Raise(new NavigateNotification(true));
            });
        }

        public ReactiveCommand CreateCommand { get; private set; }
        public ReactiveCommand CancelCommand { get; private set; }

        public ReactiveProperty<string> Title { get; private set; }
        public ReactiveProperty<string> Query { get; private set; }

    }
}
