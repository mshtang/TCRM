using System.Collections.Generic;
using TCRMDataManager.Library.Internal.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            var sql = new SqlDataAccess();
            return sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", null, "TCRMData");
        }
    }
}
