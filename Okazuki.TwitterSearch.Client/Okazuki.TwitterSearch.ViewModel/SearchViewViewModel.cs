using System;
using Codeplex.Reactive;
using System.Reactive.Linq;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Okazuki.TwitterSearch.Model;
using Okazuki.TwitterSearch.ViewModel.Interactivity;
using TweetSharp;
using System.Linq;
using Prism = Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Okazuki.TwitterSearch.ViewModel
{
    public class SearchViewViewModel : PageViewModel
    {
        public SearchViewViewModel() : base("検索結果")
        {
            this.SearchResults = new ReactiveCollection<TweetViewModel>();

            this.SelectedTweetViewModel = new ReactiveProperty<TweetViewModel>();

            this.Query = new ReactiveProperty<SearchQuery>();

            this.QueryCommand = this.IsBusy.Select(b => !b).ToReactiveCommand();
            this.QueryCommand
                .Do(_ => this.IsBusy.Value = true)
                .Subscribe(_ =>
                {
                    this.SearchResults.Clear();
                    this.ProcessSearchResult(this.Query.Value.Search());
                });

            this.SearchNextCommand = this.IsBusy.Select(b => !b).ToReactiveCommand();
            this.SearchNextCommand
                .Do(_ => this.IsBusy.Value = true)
                .Subscribe(_ =>
                {
                    this.SearchResults.RemoveAt(this.SearchResults.Count - 1);
                    this.ProcessSearchResult(this.Query.Value.SearchNext());
                });

            this.ShareTweetCommand =
                this.IsBusy
                    .Select(b => !b)
                    .CombineLatest(
                        this.SelectedTweetViewModel.Select(m => m != null),
                        (l, r) => l && r)
                    .ToReactiveCommand(false);
            this.ShareTweetCommand
                .Subscribe(_ =>
                {
                    this.ShareTweetRequest.Raise(
                        new ShareLinkNotification
                        {
                            Title = this.SelectedTweetViewModel.Value.AuthoerScreenName.Value + "の呟き",
                            Uri = this.SelectedTweetViewModel.Value.StatusUri.Value,
                            Content =""
                        });
                });

            this.RetweetCommand =
                this.IsBusy
                    .Select(b => !b)
                    .CombineLatest(
                        this.SelectedTweetViewModel.Select(m => m != null),
                        (l, r) => l && r)
                    .ToReactiveCommand(false);
            this.RetweetCommand
                .Subscribe(_ =>
                {
                    this.TweetRequest.Raise(
                        new Prism.Notification
                        {
                            Content = string.Format(" RT @{0}: {1}",
                                this.SelectedTweetViewModel.Value.AuthoerScreenName.Value,
                                this.SelectedTweetViewModel.Value.Text.Value)
                        });
                });

            this.TweetCommand = this.IsBusy.Select(b => !b).ToReactiveCommand(false);
            this.TweetCommand
                .Subscribe(_ =>
                {
                    this.TweetRequest.Raise(
                        new Prism.Notification());
                });
        }

        public SearchViewViewModel(SearchQuery query) : this()
        {
            this.Query.Value = query;
        }

        public ReactiveCollection<TweetViewModel> SearchResults { get; private set; }

        #region ShareTweetRequestプロパティ
        private InteractionRequest<ShareLinkNotification> _shareTweetRequest = new InteractionRequest<ShareLinkNotification>();
        public InteractionRequest<ShareLinkNotification> ShareTweetRequest
        {
            get { return _shareTweetRequest; }
        }
        #endregion

        #region TweetRequestプロパティ
        private InteractionRequest<Prism.Notification> _tweetRequest = new InteractionRequest<Prism.Notification>();
        public InteractionRequest<Prism.Notification> TweetRequest
        {
            get { return _tweetRequest; }
        }
        #endregion


        public ReactiveProperty<TweetViewModel> SelectedTweetViewModel { get; private set; }
        public ReactiveProperty<SearchQuery> Query { get; private set; }

        public ReactiveCommand QueryCommand { get; private set; }
        public ReactiveCommand RetweetCommand { get; private set; }

        private void ProcessSearchResult(IObservable<TwitterSearchStatus> results)
        {
            results
                .Timeout(TimeSpan.FromSeconds(20))
                .Finally(() =>
                {
                    this.SearchResults.AddOnScheduler(new SearchNextViewModel(this));
                })
                .SubscribeOn(UIDispatcherScheduler.Default)
                .Subscribe(
                    r => this.SearchResults.AddOnScheduler(new TweetViewModel(r))
                    , ex =>
                    {
                        var message = "ツイートの取得に失敗しました";
                        if (ex is TimeoutException)
                        {
                            message = "タイムアウトしました";
                        }

                        this.MessageBoxRequest.Raise(
                            new Microsoft.Practices.Prism.Interactivity.InteractionRequest.Notification
                            {
                                Title = "エラー",
                                Content = message
                            });
                        this.IsBusy.Value = false;
                    }, 
                    () =>
                    {
                        this.IsBusy.Value = false;
                    });
        }

        public ReactiveCommand SearchNextCommand { get; private set; }

        public ReactiveCommand ShareTweetCommand { get; private set; }

        public ReactiveCommand TweetCommand { get; private set; }
    }
}
