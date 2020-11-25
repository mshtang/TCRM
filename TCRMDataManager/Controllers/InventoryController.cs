using System.Collections.Generic;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        public List<Inventory> Get()
        {
            var data = new InventoryData();
            return data.GetInventory();
        }

        public void Post(Inventory item)
        {
            var data = new InventoryData();
            data.SaveInventoryRecord(item);
        }
    }
}
