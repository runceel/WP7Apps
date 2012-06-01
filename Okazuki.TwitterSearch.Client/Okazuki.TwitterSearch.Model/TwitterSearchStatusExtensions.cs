namespace Okazuki.TwitterSearch.Model
{
    using System;
    using TweetSharp;

    public static class TwitterSearchStatusExtensions
    {
        private static readonly string TwitterStatusUrlFormat = "https://twitter.com/#!/{0}/status/{1}";

        public static Uri GetStatusUri(this TwitterSearchStatus self)
        {
            // https://twitter.com/#!/okazuki/status/118219554622148610
            return new Uri(string.Format(TwitterStatusUrlFormat, self.Author.ScreenName, self.Id), UriKind.Absolute);
        }
    }
}
