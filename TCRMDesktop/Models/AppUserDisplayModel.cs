using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;

namespace TCRMDesktopUI.Models
{
    public class AppUserDisplayModel : Screen
    {
        public string Id { get; set; }

        public string Email { get; set; }

        private Dictionary<string, string> _roles;
        public Dictionary<string, string> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                NotifyOfPropertyChange();
            }
        }

        public string RoleValues
        {
            get => string.Join(" ", Roles.Select(r => r.Value));
        }
    }
}
