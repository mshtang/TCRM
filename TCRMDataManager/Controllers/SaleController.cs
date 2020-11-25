using Microsoft.AspNet.Identity;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(Sale sale)
        {
            var data = new SaleData();
            string cashierId = RequestContext.Principal.Identity.GetUserId();
            data.SaveSale(sale, cashierId);
        }
    }
}
