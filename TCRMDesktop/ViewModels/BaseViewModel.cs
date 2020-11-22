using Caliburn.Micro;
using TCRMDesktopUI.EventModels;

namespace TCRMDesktopUI.ViewModels
{
    public class BaseViewModel : Conductor<object>, IHandle<LogInEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _events;

        public BaseViewModel(IEventAggregator events, SalesViewModel salesVM)
        {
            _events = events;
            _events.Subscribe(this);

            _salesVM = salesVM;

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogInEvent logInEvent)
        {
            ActivateItem(_salesVM);
        }
    }
}
