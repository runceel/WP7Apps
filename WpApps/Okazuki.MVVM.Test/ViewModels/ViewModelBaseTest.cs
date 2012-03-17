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
using Okazuki.MVVM.ViewModels;

namespace Okazuki.MVVM.Test.ViewModels
{
    [TestClass]
    public class ViewModelBaseTest
    {
        [TestMethod]
        public void SetPropertyTest()
        {
            var target = new Target();
            Assert.IsFalse(target.IsSetAge);
            Assert.AreEqual(0, target.Age);

            target.Age = 10;
            Assert.IsTrue(target.IsSetAge);
            Assert.AreEqual(10, target.Age);

            target.IsSetAge = false;
            target.Age = 10;
            Assert.IsFalse(target.IsSetAge);
            Assert.AreEqual(10, target.Age);
        }

    }

    class Target : OkazukiViewModelBase
    {
        public bool IsSetAge { get; set; }

        private int _Age;
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                this.IsSetAge = this.SetProperty<int>(() => Age, ref _Age, value);
            }
        }
    }
}
