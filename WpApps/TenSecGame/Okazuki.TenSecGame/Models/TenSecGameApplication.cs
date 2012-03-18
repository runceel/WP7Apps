namespace Okazuki.TenSecGame.Models
{
    using System.IO.IsolatedStorage;
    using Okazuki.MVVM.Commons;
    using System.Runtime.Serialization;
    using System.IO;

    public class TenSecGameApplication : NotificationObject
    {
        public static TenSecGameApplication Context { get; private set; }

        static TenSecGameApplication()
        {
            Context = new TenSecGameApplication();
        }

        public TenSecGameApplication()
        {
            Context = this;
        }

        private TenSecGame _Game;
        public TenSecGame Game
        {
            get
            {
                return _Game;
            }
            set
            {
                this.SetProperty<TenSecGame>(() => Game, ref _Game, value);
            }
        }

        public void Load()
        {
            var file = IsolatedStorageFile.GetUserStoreForApplication();
            if (file.FileExists("data.dat"))
            {
                try
                {
                    using (var fs = file.OpenFile("data.dat", FileMode.Open))
                    {
                        var dc = new DataContractSerializer(typeof(TenSecGame));
                        this.Game = dc.ReadObject(fs) as TenSecGame;
                    }
                }
                catch
                {
                    this.Game = new TenSecGame();
                }
            }
            else
            {
                this.Game = new TenSecGame();
            }
        }

        public void Save()
        {
            var file = IsolatedStorageFile.GetUserStoreForApplication();
            using (var fs = file.OpenFile("data.dat", FileMode.Create))
            {
                var dc = new DataContractSerializer(typeof(TenSecGame));
                dc.WriteObject(fs, this.Game);
                fs.Flush();
            }
        }

    }
}
