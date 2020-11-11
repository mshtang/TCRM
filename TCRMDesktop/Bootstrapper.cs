using Caliburn.Micro;
using System.Windows;
using TCRMDesktopUI.ViewModels;

namespace TCRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<BaseViewModel>();
        }
    }
}
