using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Controllers
{

    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SalePost sale)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var data = new SaleData();

            data.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public List<Sale> GetSalesReport()
        {
            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
