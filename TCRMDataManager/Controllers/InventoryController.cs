using System.Collections.Generic;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        [Authorize(Roles = "Admin,Manager")]
        public List<Inventory> Get()
        {
            var data = new InventoryData();
            return data.GetInventory();
        }

        [Authorize(Roles = "Admin")]
        public void Post(Inventory item)
        {
            var data = new InventoryData();
            data.SaveInventoryRecord(item);
        }
    }
}
