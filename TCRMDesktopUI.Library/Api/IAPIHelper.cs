using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}