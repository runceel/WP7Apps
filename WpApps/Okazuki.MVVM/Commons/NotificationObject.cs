namespace Okazuki.MVVM.Commons
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            this.RaisePropertyChanged(ExpressionHelper.GetPropertyName(propertySelector));
        }

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
    }
}
