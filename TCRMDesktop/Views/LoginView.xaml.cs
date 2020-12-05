using System.Windows;
using System.Windows.Controls;
#if DEBUG
#endif

namespace TCRMDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

#if DEBUG

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void AutoLoginForDebug(object sender, RoutedEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //var vm = DataContext as LoginViewModel;
            //vm.UserName = "tmshdl@outlook.com";
            //vm.Password = "AdminTest.1";
            //await vm.LogIn();
        }
#else
        private void AutoLoginForDebug(object sender, RoutedEventArgs e) { }
#endif

    }
}
