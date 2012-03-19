namespace Okazuki.TenSecGame.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Okazuki.MVVM.Commons;
    using System.Threading;

    public class TenSecGame : NotificationObject
    {
        private IScheduler scheduler;

        private GameLog _CurrentGameLog;
        private GameLog CurrentGameLog
        {
            get
            {
                return _CurrentGameLog;
            }
            set
            {
                this.SetProperty<GameLog>(() => CurrentGameLog, ref _CurrentGameLog, value);
            }
        }

        public TenSecGame()
            : this(Scheduler.CurrentThread)
        {
        }

        public TenSecGame(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        private ObservableCollection<GameLog> _GameLogs = new ObservableCollection<GameLog>();
        public ObservableCollection<GameLog> GameLogs
        {
            get
            {
                return _GameLogs;
            }
            set
            {
                this.SetProperty<ObservableCollection<GameLog>>(() => GameLogs, ref _GameLogs, value);
                this.RaisePropertyChanged(() => IsStarted);
            }
        }

        public bool IsStarted { get { return this.CurrentGameLog != null; } }

        public IObservable<GameLog> GameEndAsObservable()
        {
            return this.GameLogs.CollectionAddAsObservable<GameLog>().Select(a => a.NewItems.First());
        }

        public void Start()
        {
            this.CurrentGameLog = new GameLog { GameDateTime = this.GetNowDateTime() };
        }

        public void Stop()
        {
            if (this.CurrentGameLog == null)
            {
                throw new InvalidOperationException("game does not started");
            }

            this.CurrentGameLog.GameEnd(this.GetNowDateTime());
            this.GameLogs.Insert(0, this.CurrentGameLog);
            this.CurrentGameLog = null;
        }

        private DateTime GetNowDateTime()
        {
            var format = "yyyy/MM/dd HH:mm:ss.FF";
            var tmp = this.scheduler.Now.ToString(format);
            return DateTime.ParseExact(tmp, format, Thread.CurrentThread.CurrentCulture);
        }
    }
}
