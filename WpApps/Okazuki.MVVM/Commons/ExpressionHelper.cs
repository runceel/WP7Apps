namespace Okazuki.MVVM.Commons
{
    using System;
    using System.Linq.Expressions;

    internal static class ExpressionHelper
    {
        public static string GetPropertyName<T>(Expression<T> propertySelector)
        {
            var member = propertySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException("propertySelector");
            }

            return member.Member.Name;
        }
    }
}
