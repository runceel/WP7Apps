namespace Okazuki.TwitterSearch.Model
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Phone.Reactive;
    using TweetSharp;

    public class TwitterSearchClient
    {
        private const string ConsumerKey = "QquUiNYyHYcUnMblzxyA";
        private const string ConsumerSecret = "eCAetuUybNaUC0kPjtZceAumdVr13WGdwo6EHPRIas";

        private TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);

        public IObservable<TwitterSearchStatus> Search(string query, int page, int rpp)
        {
            var subject = new AsyncSubject<IEnumerable<TwitterSearchStatus>>();
            this.service.Search(query, page, rpp, (tr, res) =>
            {
                if (res.InnerException == null)
                {
                    subject.OnNext(tr.Statuses);
                    subject.OnCompleted();
                }
                else
                {
                    subject.OnError(res.InnerException);
                }
            });
            return subject.AsObservable().SelectMany(r => r);
        }
    }
}
