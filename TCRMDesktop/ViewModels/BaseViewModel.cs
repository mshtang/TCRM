using Caliburn.Micro;

namespace TCRMDesktopUI.ViewModels
{
    public class BaseViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;

        public BaseViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            ActivateItem(_loginVM);
        }
    }
}
