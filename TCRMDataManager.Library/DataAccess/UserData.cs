using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public User GetUserById(string id)
        {
            using (var context = new TCRMContext("TCRMData"))
            {
                return context.Users.Find(id);
            }
        }
    }
}
