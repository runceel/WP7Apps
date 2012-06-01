using Microsoft.Practices.Prism.ViewModel;

namespace Okazuki.TwitterSearch.ViewModel
{
    public class ViewModelLocator : NotificationObject
    {
        public static ViewModelLocator Current { get; private set; }

        public ViewModelLocator()
        {
            Current = this;
        }

        private MainViewModel _main;

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                if (this._main == null)
                {
                    this.CreateMain();
                }

                return _main;
            }

            set
            {
                this._main = value;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public void ClearMain()
        {
            _main.Cleanup();
            _main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public void CreateMain()
        {
            if (_main == null)
            {
                _main = new MainViewModel();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public void Cleanup()
        {
            ClearMain();
        }
    }
}
