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
using System.Windows.Interactivity;

namespace Okazuki.MVVM.Behaviors
{
    public class TextBoxPropertyChangedUpdateSourceBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            if (binding == null)
            {
                return;
            }

            binding.UpdateSource();
        }
    }
}
