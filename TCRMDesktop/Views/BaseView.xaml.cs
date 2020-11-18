using System.Windows;
using TCRMDesktopUI.ViewModels;

namespace TCRMDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for BaseView.xaml
    /// </summary>
    public partial class BaseView : Window
    {
        public BaseView(BaseViewModel baseViewModel)
        {
            DataContext = baseViewModel;
            InitializeComponent();
        }
    }
}
