using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<Inventory> GetInventory()
        {
            var context = new TCRMContext("TCRMData");
            return context.Inventories.ToList();
        }

        public void SaveInventoryRecord(Inventory item)
        {
            var context = new TCRMContext("TCRMData");
            context.Inventories.Add(item);
            context.SaveChanges();
        }
    }
}
