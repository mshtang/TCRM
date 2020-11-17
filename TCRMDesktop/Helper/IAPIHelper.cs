using System.Threading.Tasks;
using TCRMDesktopUI.Models;

namespace TCRMDesktopUI.Helper
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}