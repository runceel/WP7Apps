namespace Okazuki.TwitterSearch.ViewModel
{
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Reactive.Linq;
    using Codeplex.Reactive;
    using Okazuki.TwitterSearch.Model;

    public class MainViewModel : ViewModelBase
    {
        public static readonly string QueriesFileName = "queries.xml";

        public static readonly string ErrorReportFileName = "errorReport.txt";

        public MainViewModel()
        {
            this.MainPage = new ReactiveProperty<MainPageViewModel>();
            this.CreateSearchQuery = new ReactiveProperty<CreateSearchQueryViewModel>();
            this.EditQuery = new ReactiveProperty<EditQueryViewModel>();
            this.Title = new ReactiveProperty<string>();

            this.MainPage.Value = new MainPageViewModel();
        }

        public void LoadData()
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!store.FileExists(QueriesFileName))
                    {
                        this.MainPage.Value.Queries = new ReactiveCollection<SearchQueryViewModel>();
                        this.MainPage.Value.Queries.Add(
                            new SearchQueryViewModel(this.MainPage.Value, new SearchQuery
                            {
                                Title = "作者の呟き",
                                Query = "okazuki -RT"
                            }));
                        this.MainPage.Value.Queries.Add(
                            new SearchQueryViewModel(this.MainPage.Value, new SearchQuery
                            {
                                Title = "Windows Phone 7",
                                Query = "#is12t OR #wp7jp -RT"
                            }));
                        return;
                    }

                    using (var stream = store.OpenFile(QueriesFileName, FileMode.Open))
                    {
                        var queries = SearchQuery.Load(stream);
                        this.MainPage.Value.Queries = queries.ToObservable()
                            .Select(q => new SearchQueryViewModel(this.MainPage.Value, q))
                            .ToReactiveCollection();
                    }
                }
            }
            finally
            {
                // 念のため
                if (this.MainPage.Value.Queries == null)
                {
                    this.MainPage.Value.Queries = new ReactiveCollection<SearchQueryViewModel>();
                }
            }
        }

        public void SaveData()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = store.OpenFile(QueriesFileName, FileMode.Create))
                {
                    var queries = this.MainPage.Value.Queries.Select(q => q.Model.Value);
                    SearchQuery.Save(queries, stream);
                }
            }
        }

        public ReactiveProperty<string> Title { get; private set; }

        public ReactiveProperty<MainPageViewModel> MainPage { get; private set; }

        public ReactiveProperty<CreateSearchQueryViewModel> CreateSearchQuery { get; private set; }

        public ReactiveProperty<EditQueryViewModel> EditQuery { get; private set; }

    }
}
