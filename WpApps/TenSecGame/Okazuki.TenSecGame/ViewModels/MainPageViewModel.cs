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
using GalaSoft.MvvmLight.Command;
using Okazuki.MVVM.Messages;
using System.Collections.ObjectModel;
using Okazuki.TenSecGame.Models;
using Okazuki.MVVM.Commons;
using System.Linq;
using System.Reactive.Disposables;

namespace Okazuki.TenSecGame.ViewModels
{
    public class MainPageViewModel : OkazukiViewModelBase
    {
        private CompositeDisposable disposable = new CompositeDisposable();

        public RelayCommand StartCommand { get; private set; }
        public RelayCommand ClearLogCommand { get; private set; }

        private ObservableCollection<GameLogViewModel> _GameLogs;
        public ObservableCollection<GameLogViewModel> GameLogs
        {
            get
            {
                return _GameLogs;
            }
            private set
            {
                this.SetProperty<ObservableCollection<GameLogViewModel>>(() => GameLogs, ref _GameLogs, value);
            }
        }

        private bool _HasGameLog;
        public bool HasGameLog
        {
            get
            {
                return _HasGameLog;
            }
            set
            {
                this.SetProperty<bool>(() => HasGameLog, ref _HasGameLog, value);
            }
        }


        public MainPageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            var model = TenSecGameApplication.Context;

            // ゲームログの取得
            this.GameLogs = model
                .Game
                .GameLogs
                .Select(m => new GameLogViewModel(m, this.MessageToken))
                .ToObservableCollection();

            // コレクションの同期
            var addSubscriber = model.Game.GameLogs.CollectionAddAsObservable<GameLog>()
                .Subscribe(item =>
                    this.GameLogs.Insert(
                        item.NewStartingIndex,
                        new GameLogViewModel(item.NewItems.First(), this.MessageToken)));
            var replaceSubscriber = model.Game.GameLogs.CollectionReplaceAsObservable<GameLog>()
                .Subscribe(item =>
                    this.GameLogs[item.Index] = new GameLogViewModel(item.NewItem, this.MessageToken));
            var removeSubscriber = model.Game.GameLogs.CollectionRemoveAsObservable<GameLog>()
                .Subscribe(item => this.GameLogs.RemoveAt(item.OldStartingIndex));
            var resetSubscriber = model.Game.GameLogs.CollectionResetAsObservable()
                .Subscribe(_ =>
                {
                    this.GameLogs.Clear();
                    foreach (var m in model.Game.GameLogs)
                    {
                        this.GameLogs.Add(new GameLogViewModel(m, this.MessageToken));
                    }
                });
            // 解放処理
            disposable.Add(addSubscriber);
            disposable.Add(replaceSubscriber);
            disposable.Add(removeSubscriber);
            disposable.Add(resetSubscriber);

            // ゲーム履歴の有無
            this.HasGameLog = model.Game.GameLogs.Count != 0;
            var changedSubscriber = model.Game.GameLogs.CollectionChangedAsObservable()
                .Subscribe(_ => this.HasGameLog = model.Game.GameLogs.Count != 0);
            disposable.Add(changedSubscriber);


            // コマンドの初期化
            this.StartCommand = new RelayCommand(() =>
            {
                this.Messenger.SendWithViewModelToken(
                    this,
                    new NavigationMessage("/Views/GamePage.xaml"));
            });
            this.ClearLogCommand = new RelayCommand(() =>
            {
                model.Game.GameLogs.Clear();
            });
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.disposable.Dispose();
        }
    }
}
