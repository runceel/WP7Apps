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
using Okazuki.MVVM.ViewModels;
using Okazuki.TenSecGame.Models;

namespace Okazuki.TenSecGame.ViewModels
{
    public class GameLogViewModel : OkazukiViewModelBase
    {
        private GameLog _Model;
        public GameLog Model
        {
            get
            {
                return _Model;
            }
            set
            {
                this.SetProperty<GameLog>(() => Model, ref _Model, value);
            }
        }

        public GameLogViewModel(GameLog model, Guid messageToken)
            : base(messageToken)
        {
        }
    }
}
