using System.Windows.Navigation;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Okazuki.TwitterSearch.ViewModel.Interactivity;
using Codeplex.Reactive;

namespace Okazuki.TwitterSearch.ViewModel
{
    public interface IPageLifecycle
    {
        void NavigateFrom(NavigationMode navigationMode);
        void NavigateTo();
    }

    public class PageViewModel : ViewModelBase, IPageLifecycle
    {
        public PageViewModel()
        {
            this.PageName = new ReactiveProperty<string>();
            this.IsBusy = new ReactiveProperty<bool>();
        }

        protected PageViewModel(string pageName) : this()
        {
            this.PageName.Value = pageName;
        }

        public ReactiveProperty<string> PageName { get; private set; }

        public ReactiveProperty<bool> IsBusy { get; private set; }

        private InteractionRequest<NavigateNotification> _navigateRequest = new InteractionRequest<NavigateNotification>();

        public InteractionRequest<NavigateNotification> NavigateRequest
        {
            get { return this._navigateRequest; }
        }

        private InteractionRequest<Notification> _messageBoxRequest = new InteractionRequest<Notification>();

        public InteractionRequest<Notification> MessageBoxRequest
        {
            get { return this._messageBoxRequest; }
        }

        #region IPageLifecycle メンバー

        public virtual void NavigateFrom(NavigationMode navigationMode)
        {
        }

        public virtual void NavigateTo()
        {
        }

        #endregion
    }
}
