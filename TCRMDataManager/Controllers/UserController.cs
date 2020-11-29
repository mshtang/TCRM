using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TCRMDataManager.Library.DataAccess;
using TCRMDataManager.Library.Models;
using TCRMDataManager.Models;

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

        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
#if DEBUG
        [AllowAnonymous]
#else
        [Authorize(Roles = "Admin")]
#endif
        public List<AppUserModel> GetAllUsers()
        {
            var appUserRes = new List<AppUserModel>();

            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                foreach (var user in users)
                {
                    var appUser = new AppUserModel
                    {
                        Id = user.Id,
                        Email = user.Email
                    };

                    foreach (var role in user.Roles)
                    {
                        appUser.Roles.Add(role.RoleId, roles.Where(x => x.Id == role.RoleId).First().Name);
                    }

                    appUserRes.Add(appUser);
                }
            }

            return appUserRes;
        }

        [HttpGet]
        [Route("api/User/Admin/GetAllRoles")]
#if DEBUG
        [AllowAnonymous]
#else
        [Authorize(Roles = "Admin")]
#endif
        public List<string> GetAllRoles()
        {
            var roleRes = new List<string>();

            using (var context = new ApplicationDbContext())
            {
                var roles = context.Roles.ToList();
                roleRes = roles.Select(r => r.Name).ToList();
            }

            return roleRes;
        }
    }
}
