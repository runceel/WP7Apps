namespace Okazuki.TwitterSearch.ViewModel
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using Codeplex.Reactive;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Okazuki.TwitterSearch.ViewModel.Interactivity;
    using Prism = Microsoft.Practices.Prism.Interactivity.InteractionRequest;

    public class MainPageViewModel : PageViewModel
    {
        public MainPageViewModel() : base("ついさち")
        {
            this.Queries = new ReactiveCollection<SearchQueryViewModel>();
            this.EditTargetQuery = new ReactiveProperty<SearchQueryViewModel>();
            this.SelectedQuery = new ReactiveProperty<SearchViewViewModel>();

            this.AddCommand = new ReactiveCommand();
            this.AddCommand.Subscribe(_ =>
            {
                ViewModelLocator.Current.Main.CreateSearchQuery.Value = new CreateSearchQueryViewModel(this);
                this.NavigateRequest.Raise(
                new NavigateNotification(new Uri("/CreateSearchQueryView.xaml", UriKind.Relative)));

            });

            this.RemoveCommand = this.EditTargetQuery.Select(vm => vm != null).ToReactiveCommand();
            this.RemoveCommand.Subscribe(_ =>
            {
                this.Queries.Remove(this.EditTargetQuery.Value);
                this.EditTargetQuery.Value = null;
                ViewModelLocator.Current.Main.SaveData();

            });

            this.UpCommand = this.EditTargetQuery.Select(v =>
                v != null && v != this.Queries.FirstOrDefault())
                .ToReactiveCommand();
            this.UpCommand.Subscribe(_ =>
            {
                var index = this.Queries.IndexOf(this.EditTargetQuery.Value);
                var target = this.EditTargetQuery.Value;
                this.Queries.Remove(target);
                this.Queries.Insert(index - 1, target);
                this.EditTargetQuery.Value = target;
                ViewModelLocator.Current.Main.SaveData();
            });

            this.DownCommand = this.EditTargetQuery.Select(v =>
                v != null && v != this.Queries.LastOrDefault())
                .ToReactiveCommand();
            this.DownCommand.Subscribe(_ =>
            {
                var index = this.Queries.IndexOf(this.EditTargetQuery.Value);
                var target = this.EditTargetQuery.Value;
                this.Queries.Remove(target);
                this.Queries.Insert(index + 1, target);
                this.EditTargetQuery.Value = target;
                ViewModelLocator.Current.Main.SaveData();
            });

            this.EditCommand = this.EditTargetQuery.Select(vm => vm != null).ToReactiveCommand();
            this.EditCommand.Subscribe(_ =>
            {
                ViewModelLocator.Current.Main.EditQuery.Value =
                    new EditQueryViewModel(this.EditTargetQuery.Value.Model.Value);

                this.NavigateRequest.Raise(
                    new NavigateNotification(new Uri("/EditSearchQueryView.xaml", UriKind.Relative)));
            });

            this.TweetCommand = new ReactiveCommand();
            this.TweetCommand
                .Subscribe(_ =>
                {
                    this.TweetRequest.Raise(
                        new Prism.Notification());
                });
        }

        public override void NavigateTo()
        {
            ViewModelLocator.Current.Main.SaveData();
        }

        public override void NavigateFrom(System.Windows.Navigation.NavigationMode navigationMode)
        {
            ViewModelLocator.Current.Main.SaveData();
        }

        #region TweetRequestプロパティ
        private InteractionRequest<Prism.Notification> _tweetRequest = new InteractionRequest<Prism.Notification>();
        public InteractionRequest<Prism.Notification> TweetRequest
        {
            get { return _tweetRequest; }
        }
        #endregion

        public ReactiveCollection<SearchQueryViewModel> Queries { get; set; }
        public ReactiveProperty<SearchQueryViewModel> EditTargetQuery { get; private set; }
        public ReactiveProperty<SearchViewViewModel> SelectedQuery { get; private set; }
        public ReactiveCommand AddCommand { get; private set; }
        public ReactiveCommand RemoveCommand { get; private set; }
        public ReactiveCommand UpCommand { get; private set; }
        public ReactiveCommand DownCommand { get; private set; }
        public ReactiveCommand EditCommand { get; private set; }
        public ReactiveCommand TweetCommand { get; private set; }
    }
}
