using Caliburn.Micro;
using System.Collections.ObjectModel;
using TCRMDesktopUI.Library.Api;

namespace TCRMDesktopUI.Models
{
    public class AppUserDisplayModel : Screen
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ObservableCollection<RoleViewModel> Roles { get; set; } = new ObservableCollection<RoleViewModel>();
    }

    public class RoleViewModel : Screen
    {
        private IUserEndpoint _userEndpoint;
        private string _userId;

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange();
                if (value == true)
                {
                    _userEndpoint.AddRole(_userId, RoleName);
                }
                else
                {
                    _userEndpoint.RemoveRole(_userId, RoleName);
                }
            }
        }

        public RoleViewModel() { }

        public RoleViewModel(IUserEndpoint userEndpoint, string userId)
        {
            _userEndpoint = userEndpoint;
            _userId = userId;
        }
    }
}

