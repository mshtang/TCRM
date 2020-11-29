using System.Collections.Generic;

namespace TCRMDesktopUI.Library.Models
{
    public class AppUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
    }
}
