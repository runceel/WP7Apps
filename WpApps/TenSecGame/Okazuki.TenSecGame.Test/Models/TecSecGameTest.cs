namespace Okazuki.TenSecGame.Test.Models
{
    using System;
    using System.IO;
    using System.Reactive.Concurrency;
    using System.Runtime.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Okazuki.TenSecGame.Models;

    [TestClass]
    public class TecSecGameTest
    {
        private static readonly DateTimeOffset GameStartDateTimeOffset = DateTimeOffset.Parse("2013/12/01");
        private HistoricalScheduler scheduler;
        private Okazuki.TenSecGame.Models.TenSecGame game;

        [TestInitialize]
        public void SetUp()
        {
            this.scheduler = new HistoricalScheduler();
            scheduler.AdvanceTo(GameStartDateTimeOffset);
            this.game = new Okazuki.TenSecGame.Models.TenSecGame(scheduler);
        }

        [TestCleanup]
        public void TearDown()
        {
            this.game = null;
            this.scheduler = null;
        }

        [TestMethod]
        public void JustTenSec()
        {
            var log = default(GameLog);
            game.GameEndAsObservable().Subscribe(l => log = l);
            game.Start();

            scheduler.AdvanceBy(TimeSpan.FromSeconds(10));
            game.Stop();

            log.Is(l => l.GameDateTime == GameStartDateTimeOffset && l.TenSecSpan == TimeSpan.Zero && l.IsPerfect);
        }

        [TestMethod]
        public void ManyGame()
        {
            game.Start();
            scheduler.AdvanceBy(TimeSpan.FromSeconds(9.9));
            game.Stop();

            game.Start();
            scheduler.AdvanceBy(TimeSpan.FromSeconds(11.1));
            game.Stop();

            game.GameLogs.Count.Is(2);

            game.GameLogs[0].Is(i =>
                i.TenSecSpan == TimeSpan.FromSeconds(-1.1) &&
                i.GameDateTime == GameStartDateTimeOffset.Add(TimeSpan.FromSeconds(9.9)).DateTime,
                "後にしたゲームの情報が先頭に追加される");

            game.GameLogs[1].Is(i => 
                i.TenSecSpan == TimeSpan.FromSeconds(0.1) &&
                i.GameDateTime == GameStartDateTimeOffset.DateTime);

        }

        [TestMethod]
        public void SerializeTest()
        {
            game.Start();
            scheduler.AdvanceBy(TimeSpan.FromSeconds(9));
            game.Stop();

            game.Start();
            scheduler.AdvanceBy(TimeSpan.FromSeconds(10));
            game.Stop();

            var serializer = new DataContractSerializer(typeof(Okazuki.TenSecGame.Models.TenSecGame));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, game);
            ms.Seek(0, SeekOrigin.Begin);

            var deserializedGame = serializer.ReadObject(ms) as Okazuki.TenSecGame.Models.TenSecGame;
            deserializedGame.Is(i =>
                i.GameLogs.Count == 2 &&
                i.GameLogs[0].GameDateTime == GameStartDateTimeOffset.Add(TimeSpan.FromSeconds(9)) &&
                i.GameLogs[0].TenSecSpan == TimeSpan.Zero &&
                i.GameLogs[1].GameDateTime == GameStartDateTimeOffset.DateTime &&
                i.GameLogs[1].TenSecSpan == TimeSpan.FromSeconds(1));
        }
    }
}
