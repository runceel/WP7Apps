namespace Okazuki.SearchHub.Models
{
    using Okazuki.MVVM.Commons;

    /// <summary>
    /// お気に入りアイテム
    /// </summary>
    public class FavItem : NotificationObject
    {
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                this.SetProperty<string>(() => Title, ref _Title, value);
            }
        }

        private string _Url;
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                this.SetProperty<string>(() => Url, ref _Url, value);
            }
        }

    }
}
