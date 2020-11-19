using Caliburn.Micro;
using TCRMDesktopUI.EventModels;

namespace TCRMDesktopUI.ViewModels
{
    public class BaseViewModel : Conductor<object>, IHandle<LogInEvent>
    {
        private SalesViewModel _salesVM;
        private SimpleContainer _container;
        private IEventAggregator _events;

        public BaseViewModel(IEventAggregator events, SimpleContainer container, SalesViewModel salesVM)
        {
            _events = events;
            _events.Subscribe(this);

            _container = container;

            _salesVM = salesVM;

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogInEvent logInEvent)
        {
            ActivateItem(_salesVM);
        }
    }
}
