using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<Product> GetProducts()
        {
            using (var context = new TCRMContext("TCRMData"))
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductById(int id)
        {
            return GetProducts().Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
