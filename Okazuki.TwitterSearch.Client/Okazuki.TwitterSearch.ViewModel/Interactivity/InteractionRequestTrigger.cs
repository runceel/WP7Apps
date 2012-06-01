namespace Okazuki.TwitterSearch.ViewModel.Interactivity
{
    using System.Windows.Interactivity;

    public class InteractionRequestTrigger : EventTrigger
    {
        protected override string GetEventName()
        {
            return "Raised";
        }
    }
}
