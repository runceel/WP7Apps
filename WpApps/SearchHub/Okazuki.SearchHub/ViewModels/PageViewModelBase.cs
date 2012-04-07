using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Okazuki.MVVM.ViewModels;
using Okazuki.SearchHub.Models;
using System.Reactive.Disposables;

namespace Okazuki.SearchHub.ViewModels
{
    public class PageViewModelBase : OkazukiViewModelBase
    {
        protected CompositeDisposable Disposable { get; private set; }
        protected SearchHubApplication Application { get; private set; }

        public PageViewModelBase()
        {
            this.Disposable = new CompositeDisposable();
            if (this.IsInDesignMode)
            {
                return;
            }

            this.Application = SearchHubApplication.Current;
        }

        public string ApplicationName
        {
            get
            {
                return SearchHubApplication.Current.Name;
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
            if (!this.Disposable.IsDisposed)
            {
                this.Disposable.Dispose();
            }
        }
    }
}
