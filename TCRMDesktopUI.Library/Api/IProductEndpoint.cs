using System.Collections.Generic;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<Product>> GetAll();
    }
}