namespace Okazuki.MVVM.Test.ViewModels
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Okazuki.MVVM.Behaviors;
    using Okazuki.MVVM.ViewModels;

    [TestClass]
    public class MessengerTest
    {
        [TestMethod]
        public void MessageTest()
        {
            var vm = new TargetViewModel();
            var target = new Button { DataContext = vm };
            var behavior = new TestBehavior { MessageToken = vm.MessageToken };
            Interaction.GetBehaviors(target).Add(behavior);

            Messenger.Default.Send<string>("OK", vm.MessageToken);
            Assert.AreEqual("OK", behavior.Message);
        }

        [TestMethod]
        public void CallbackTest()
        {
            var o = new object();

            Messenger.Default.Register<CallbackMessage>(
                o,
                Guid.Empty,
                m => m.Callback(m));

            var r = Messenger.Default.SendWithEmptyAsObservable<CallbackMessage>(
                callback => new CallbackMessage("OK", callback))
                .First();
            Assert.AreSame("OK", r.Content);
        }
    }

    class CallbackMessage : GenericMessage<string>
    {
        public Action<CallbackMessage> Callback { get; private set; }

        public CallbackMessage(string m, Action<CallbackMessage> callback) : base(m)
        {
            this.Callback = callback;
        }
    }

    class TargetViewModel : OkazukiViewModelBase { }
    class TestBehavior : MessageBehaviorBase<FrameworkElement, string>
    {
        public string Message { get; set; }
        protected override void Invoke(string message)
        {
            this.Message = message;
        }
    }

}
