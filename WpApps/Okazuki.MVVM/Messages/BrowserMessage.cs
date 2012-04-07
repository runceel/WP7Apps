namespace Okazuki.MVVM.Messages
{
    using System;
    using GalaSoft.MvvmLight.Messaging;

    public class BrowserMessage : GenericMessage<Uri>
    {
        public BrowserMessage(string url) : base(new Uri(url, UriKind.Absolute))
        {
        }
    }
}
