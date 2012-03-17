namespace Okazuki.MVVM.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using GalaSoft.MvvmLight.Messaging;
    using Okazuki.MVVM.Commons;

    public class OkazukiViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        public Guid MessageToken { get; private set; }

        public OkazukiViewModelBase() : this(Guid.NewGuid())
        {
        }

        public OkazukiViewModelBase(Guid messageToken) : base(Messenger.Default)
        {
            this.MessageToken = messageToken;
        }

        public Messenger Messenger { get { return this.MessengerInstance as Messenger; } }

        protected virtual bool SetProperty<T>(Expression<Func<T>> propertySelector, ref T field, T value)
        {
            return this.SetProperty(
                ExpressionHelper.GetPropertyName(propertySelector),
                ref field,
                value);
        }

        protected virtual bool SetProperty<T>(string propertyName, ref T field, T value)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            this.RaisePropertyChanged(ExpressionHelper.GetPropertyName(propertySelector));
        }
    }
}
