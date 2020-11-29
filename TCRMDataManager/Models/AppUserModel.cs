using System.Collections.Generic;

namespace TCRMDataManager.Models
{
    public class AppUserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
    }
}