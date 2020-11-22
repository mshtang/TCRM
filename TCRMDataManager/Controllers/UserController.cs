using Microsoft.AspNet.Identity;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public User GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var data = new UserData();
            return data.GetUserById(userId);
        }
    }
}
