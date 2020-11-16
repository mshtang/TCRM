using Caliburn.Micro;

namespace TCRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get => UserName?.Length > 0 && Password?.Length > 0;
        }

        public LoginViewModel()
        {

        }


        public void LogIn(string userName, string password)
        {

        }
    }
}
