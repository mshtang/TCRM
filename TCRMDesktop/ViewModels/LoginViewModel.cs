using Caliburn.Micro;
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

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
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
            }
        }
    }
}
