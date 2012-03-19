namespace Okazuki.TenSecGame.ViewModels
{
    using System;
    using Okazuki.MVVM.ViewModels;
    using Okazuki.TenSecGame.Models;
    using GalaSoft.MvvmLight.Command;

    public class GameLogViewModel : OkazukiViewModelBase
    {
        public RelayCommand DeleteCommand { get; private set; }

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

            this.DeleteCommand = new RelayCommand(() =>
            {
                TenSecGameApplication.Context.Game.GameLogs.Remove(this.Model);
            });
        }

        public string FormatedTenSecSpan
        {
            get
            {
                return string.Format("誤差 {0}.{1}秒", this.Model.TenSecSpan.Seconds, this.Model.TenSecSpan.Milliseconds);
            }
        }

    }
}
