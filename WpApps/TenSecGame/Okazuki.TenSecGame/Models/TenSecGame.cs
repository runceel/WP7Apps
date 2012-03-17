namespace Okazuki.TenSecGame.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using Okazuki.MVVM.Commons;

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
            this.CurrentGameLog = new GameLog { GameDateTime = this.scheduler.Now.DateTime };
        }

        public void Stop()
        {
            if (this.CurrentGameLog == null)
            {
                throw new InvalidOperationException("game does not started");
            }

            this.CurrentGameLog.GameEnd(scheduler.Now.DateTime);
            this.GameLogs.Add(this.CurrentGameLog);
            this.CurrentGameLog = null;
        }
    }
}
