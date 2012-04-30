namespace Okazuki.TwiCard
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Phone.Controls;
    using Microsoft.Xna.Framework.Media;
    using TweetSharp;
    using System.Windows.Input;

    public partial class MainPage : PhoneApplicationPage
    {
        // コンストラクター
        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButtonCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTwitterId.Text))
            {
                MessageBox.Show("Twitter IDを入力してください");
                return;
            }

            this.progressBar.Visibility = Visibility.Visible;
            this.Focus();
            var s = new TwitterService();
            s.GetUserProfileFor(textBoxTwitterId.Text, (user, resp) =>
            {
                if (resp.InnerException != null)
                {
                    this.Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show("データの取得に失敗しました");
                        this.progressBar.Visibility = Visibility.Collapsed;
                    });
                    return;
                }

                this.Dispatcher.BeginInvoke(() =>
                {
                    var profileUrl = new Uri(user.ProfileImageUrlHttps.Replace("____normal.jpg", "___.jpg"), UriKind.Absolute);
                    var source = new BitmapImage();
                    source.UriSource = profileUrl;
                    this.imageProfile.Source = source;
                    this.textBlockTwitterId.Text = "id:" + user.ScreenName;
                    this.textBlockScreenName.Text = "name: " + user.Name;
                    this.textBlockLocation.Text = "location: " + user.Location;
                    this.progressBar.Visibility = Visibility.Collapsed;
                });
            });
        }

        private void ApplicationBarIconButtonSave_Click(object sender, EventArgs e)
        {
            var wb = new WriteableBitmap(gridCard, new ScaleTransform { CenterX = 0, CenterY = 0, ScaleX = 2, ScaleY = 2 });
            var iso = IsolatedStorageFile.GetUserStoreForApplication();
            using (var fs = iso.OpenFile("tmp.jpg", FileMode.Create))
            {
                wb.SaveJpeg(fs, 800, 400, 0, 100);
            }

            var library = new MediaLibrary();
            var fileName = string.Format("twitterCard{0:yyyyMMddHHmmss}.jpg", DateTime.Now);
            using (var fs = iso.OpenFile("tmp.jpg", FileMode.Open))
            {
                library.SavePicture(fileName, fs);
            }

            MessageBox.Show(string.Format("名刺を{0}という名前で保存しました", fileName));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}