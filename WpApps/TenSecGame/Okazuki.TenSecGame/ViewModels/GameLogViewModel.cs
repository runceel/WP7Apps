namespace Okazuki.TenSecGame.ViewModels
{
    using System;
    using Okazuki.MVVM.ViewModels;
    using Okazuki.TenSecGame.Models;
    using GalaSoft.MvvmLight.Command;

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
            this.Model = model;
        }

        public string FormatedTenSecSpan
        {
            get
            {
                if (this.Model.IsPerfect)
                {
                    return "完璧！！";
                }

                return string.Format(
                    this.Model.TenSecSpan.TotalMilliseconds < 0 ? "誤差 -{0:ss.ff}秒" : "誤差 {0:ss.ff}秒",
                    new DateTime(0).Add(TimeSpan.FromMilliseconds(Math.Abs(this.Model.TenSecSpan.TotalMilliseconds))));
            }
        }

    }
}
