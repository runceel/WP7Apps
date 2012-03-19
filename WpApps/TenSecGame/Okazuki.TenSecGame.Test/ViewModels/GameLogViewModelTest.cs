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
using Okazuki.TenSecGame.Models;
using Okazuki.TenSecGame.ViewModels;

namespace Okazuki.TenSecGame.Test.ViewModels
{
    [TestClass]
    public class GameLogViewModelTest
    {
        [TestMethod]
        public void TestFormatedTenSecSpan()
        {
            var model = new GameLog { TenSecSpan = TimeSpan.FromMilliseconds(1500) };
            var viewModel = new GameLogViewModel(model, Guid.Empty);
            viewModel.FormatedTenSecSpan.Is("誤差 1.500秒");
        }

    }
}
