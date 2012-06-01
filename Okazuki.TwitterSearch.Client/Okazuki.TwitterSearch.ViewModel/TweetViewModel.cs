namespace Okazuki.TwitterSearch.ViewModel
{
    using System;
    using System.Linq;
    using Codeplex.Reactive;
    using System.Reactive.Linq;
    using Okazuki.TwitterSearch.Model;
    using TweetSharp;

    public class TweetViewModel : ViewModelBase
    {
        public TweetViewModel(TwitterSearchStatus status)
        {
            if (status == null)
            {
                return;
            }

            this.AuthoerScreenName = new ReactiveProperty<string>(status.Author.ScreenName);
            this.Text = new ReactiveProperty<string>(StringUtils.Decode(status.Text));
            this.CreatedDate = new ReactiveProperty<DateTime>(TimeZoneInfo.ConvertTime(status.CreatedDate, TimeZoneInfo.Local));
            this.ProfileImageUrl = new ReactiveProperty<string>(status.Author.ProfileImageUrl);
            this.Id = new ReactiveProperty<long>(status.Id);
            this.StatusUri = new ReactiveProperty<Uri>(status.GetStatusUri());
            this.Uris = status
                .Entities
                .Urls
                .Select(uri => new TweetUriViewModel(new Uri(uri.Value, UriKind.Absolute)))
                .ToObservable()
                .ToReactiveCollection();
        }

        public ReactiveProperty<string> AuthoerScreenName { get; private set; }
        public ReactiveProperty<string> Text { get; private set; }
        public ReactiveProperty<DateTime> CreatedDate { get; private set; }
        public ReactiveProperty<string> ProfileImageUrl { get; private set; }
        public ReactiveProperty<long> Id { get; private set; }
        public ReactiveProperty<Uri> StatusUri { get; private set; }
        public ReactiveCollection<TweetUriViewModel> Uris { get; private set; }
    }
}
