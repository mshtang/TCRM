using System.Collections.Generic;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<AppUser>> GetAll();
        Task<List<string>> GetAllRoles();
    }
}