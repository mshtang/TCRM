using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<Product> GetProducts()
        {
            var context = new TCRMContext("TCRMData");
            return context.Products.Include("Tax").ToList();
        }
    }
}
