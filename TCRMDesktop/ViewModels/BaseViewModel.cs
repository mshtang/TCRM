using Caliburn.Micro;
using TCRMDesktopUI.EventModels;
using TCRMDesktopUI.Library.Api;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class BaseViewModel : Conductor<object>, IHandle<LogInEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;

        private bool _isLoggedIn;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                NotifyOfPropertyChange();
            }
        }


        public BaseViewModel(IEventAggregator events, SalesViewModel salesVM, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _events.Subscribe(this);

            _salesVM = salesVM;
            _user = user;
            _apiHelper = apiHelper;

            ActivateItem(IoC.Get<LoginViewModel>());
            IsLoggedIn = false;
        }

        public void Handle(LogInEvent logInEvent)
        {
            ActivateItem(_salesVM);
            IsLoggedIn = true;
        }

        public void ExitApp()
        {
            TryClose();
        }

        public void ManageUsers()
        {
            ActivateItem(IoC.Get<ShowAppUserViewModel>());
        }

        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            IsLoggedIn = false;
        }
    }
}
