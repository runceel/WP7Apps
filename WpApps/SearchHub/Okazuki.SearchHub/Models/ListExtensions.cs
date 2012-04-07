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
using System.Collections.Generic;

namespace Okazuki.SearchHub.Models
{
    public static class ListExtensions
    {
        public static void Up<T>(this IList<T> self, T target)
        {
            var index = self.IndexOf(target);
            if (index == -1)
            {
                return;
            }

            if (index == 0)
            {
                return;
            }

            self.Remove(target);
            self.Insert(index - 1, target);
        }

        public static void Down<T>(this IList<T> self, T target)
        {
            var index = self.IndexOf(target);
            if (index == -1)
            {
                return;
            }

            if (index == self.Count - 1)
            {
                return;
            }

            self.Remove(target);
            self.Insert(index + 1, target);
        }
    }
}
