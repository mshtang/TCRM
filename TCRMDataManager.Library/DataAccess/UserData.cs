using System.Collections.Generic;
using TCRMDataManager.Library.Internal.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string id)
        {
            var sql = new SqlDataAccess();

            var p = new { Id = id };
            return sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "TCRMData");
        }
    }
}
