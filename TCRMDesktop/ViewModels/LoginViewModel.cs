using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using TCRMDesktopUI.EventModels;
using TCRMDesktopUI.Library.Api;

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
        public ISnackbarMessageQueue SbMessQ { get; set; }
        private IEventAggregator _events;

        public LoginViewModel()
        {

        }

        public LoginViewModel(IAPIHelper apiHelper, ISnackbarMessageQueue sbMessQ, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            SbMessQ = sbMessQ;
            _events = events;
        }

        public bool CanLogIn
        {
            get => UserName?.Length > 0 && Password?.Length > 0;
        }


        public async Task LogIn()
        {
            try
            {
                var res = await _apiHelper.Authenticate(UserName, Password);
                await _apiHelper.GetLoggedInUserInfo(res.Access_Token);
                _events.PublishOnUIThread(new LogInEvent());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                SbMessQ.Enqueue("Either your user name or your password is wrong.", "Retry", () => { UserName = string.Empty; Password = null; });
            }
        }
    }
}
