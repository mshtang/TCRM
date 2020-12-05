using System.Collections.Generic;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<AppUser>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddRole(string userId, string role);
        Task RemoveRole(string userId, string role);
    }
}