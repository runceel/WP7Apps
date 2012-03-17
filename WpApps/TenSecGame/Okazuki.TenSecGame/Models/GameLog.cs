namespace Okazuki.TenSecGame.Models
{
    using System;
    using Okazuki.MVVM.Commons;

    public class GameLog : NotificationObject
    {
        private DateTime _GameDateTime;
        /// <summary>
        /// ゲームをした日時
        /// </summary>
        public DateTime GameDateTime
        {
            get
            {
                return _GameDateTime;
            }
            set
            {
                this.SetProperty<DateTime>(() => GameDateTime, ref _GameDateTime, value);
            }
        }

        private TimeSpan _TenSecSpan;
        /// <summary>
        /// 10秒との差分
        /// </summary>
        public TimeSpan TenSecSpan
        {
            get
            {
                return _TenSecSpan;
            }
            set
            {
                this.SetProperty<TimeSpan>(() => TenSecSpan, ref _TenSecSpan, value);
            }
        }


        public void GameEnd(DateTime stopDateTime)
        {
            var span = stopDateTime - this.GameDateTime;
            this.TenSecSpan = TimeSpan.FromSeconds(10) - span;
        }
    }
}
