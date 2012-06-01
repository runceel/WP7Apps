namespace Okazuki.TwitterSearch.ViewModel
{
    using System;
    using System.Reactive.Linq;
    using Codeplex.Reactive;
    using Okazuki.TwitterSearch.Model;
    using Okazuki.TwitterSearch.ViewModel.Interactivity;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using System.Reactive;

    public class EditQueryViewModel : PageViewModel
    {
        public ReactiveProperty<string> Title { get; private set; }
        public ReactiveProperty<string> Query { get; private set; }

        public ReactiveCommand CommitCommand { get; private set; }
        public ReactiveCommand CancelCommand { get; private set; }

        // DesignTime only
        public EditQueryViewModel() : base("検索条件編集")
        {
        }

        public EditQueryViewModel(SearchQuery target) : base("検索条件編集")
        {
            this.Title = new ReactiveProperty<string>()
                .SetValidateError(s => string.IsNullOrWhiteSpace(s) ? "タイトルを入力してください" : null);
            this.Query = new ReactiveProperty<string>()
                .SetValidateError(s => string.IsNullOrWhiteSpace(s) ? "クエリを入力してください" : null);
            this.Title.Value = target.Title;
            this.Query.Value = target.Query;

            this.CancelCommand = new ReactiveCommand();
            this.CancelCommand.Subscribe(_ =>
            {
                this.NavigateRequest.Raise(new NavigateNotification(true));
            });

            this.CommitCommand = Observable.Merge(
                this.Title.Do(_ => this.Title.ForceValidate()).Select(_ => Unit.Default),
                this.Query.Do(_ => this.Query.ForceValidate()).Select(_ => Unit.Default))
                .Select(_ => string.IsNullOrWhiteSpace(this.Title.Error) && string.IsNullOrWhiteSpace(this.Query.Error))
                .ToReactiveCommand();
            this.CommitCommand.Subscribe(_ =>
            {
                target.Title = this.Title.Value;
                target.Query = this.Query.Value;
                this.NavigateRequest.Raise(new NavigateNotification(true));
            });
        }


    }
}
