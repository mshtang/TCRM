using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(Sale sale);
    }
}