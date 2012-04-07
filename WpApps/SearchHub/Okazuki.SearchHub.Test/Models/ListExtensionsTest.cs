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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Okazuki.SearchHub.Models;

namespace Okazuki.SearchHub.Test.Models
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void UpTest()
        {
            var items = new List<int> { 1, 2, 3, 4 };
            items.Up(2);
            items.Is(2, 1, 3, 4);
            items.Up(2);
            items.Is(2, 1, 3, 4);
        }

        [TestMethod]
        public void DownTest()
        {
            var items = new List<int> { 1, 2, 3, 4 };
            items.Down(3);
            items.Is(1, 2, 4, 3);
            items.Down(3);
            items.Is(1, 2, 4, 3);
        }
    }
}
