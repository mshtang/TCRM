using System.Windows;
using System.Windows.Controls;
#if DEBUG
using TCRMDesktopUI.ViewModels;
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

        private async void AutoLoginForDebug(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as LoginViewModel;
            vm.UserName = "tmshdl@outlook.com";
            vm.Password = "AdminTest.1";
            await vm.LogIn();
        }
#else
        private void AutoLoginForDebug(object sender, RoutedEventArgs e) { }
#endif

    }
}
