namespace Okazuki.TwitterSearch.ViewModel
{
    using System;
    using Codeplex.Reactive;
    using System.Reactive.Linq;
    using Okazuki.TwitterSearch.ViewModel.Interactivity;

    public class TweetUriViewModel
    {
        public ReactiveProperty<Uri> Uri { get; private set; }
        public ReactiveCommand OpenBrowserCommand { get; private set; }
        public ReactiveInteractionRequest<OpenBrowserNotification> OpenBrowserRequest { get; private set; }

        public TweetUriViewModel(Uri uri)
        {
            this.Uri = new ReactiveProperty<Uri>(uri);
            this.OpenBrowserCommand = new ReactiveCommand();
            this.OpenBrowserRequest =
                this.OpenBrowserCommand
                    .Select(_ => new OpenBrowserNotification { Uri = this.Uri.Value })
                    .ToReactiveInteractionRequest();
        }
    }
}
