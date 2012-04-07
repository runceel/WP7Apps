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

namespace Okazuki.MVVM.ViewModels
{
    public class ModelViewModel<TViewModel, TModel> : OkazukiViewModelBase
        where TViewModel : OkazukiViewModelBase
        where TModel : class
    {
        protected TViewModel Parent { get; private set; }

        public TModel Model { get; private set; }

        public ModelViewModel(TViewModel parent, TModel model) : base(parent.MessageToken)
        {
            this.Parent = parent;
            this.Model = model;
        }
    }
}
