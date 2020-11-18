using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using TCRMDesktopUI.Helper;

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

        private readonly IAPIHelper _apiHelper;
        //public SnackbarMessageQueue ErrorMessQ { get; set; }
        public ISnackbarMessageQueue ErrorMessQ { get; set; }

        public LoginViewModel(IAPIHelper apiHelper, ISnackbarMessageQueue sbMessQ)
        {
            _apiHelper = apiHelper;
            //ErrorMessQ = new SnackbarMessageQueue();
            ErrorMessQ = sbMessQ;
        }

        public bool CanLogIn
        {
            get => UserName?.Length > 0 && Password?.Length > 0;
        }

        public LoginViewModel()
        {

        }


        public async Task LogIn()
        {
            try
            {
                var res = await _apiHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                ErrorMessQ.Enqueue("Either your user name or your password is wrong.", "Retry", () => { UserName = string.Empty; Password = null; });
            }
        }
    }
}
