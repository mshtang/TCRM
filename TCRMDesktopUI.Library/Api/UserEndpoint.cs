using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private IAPIHelper _apiHelper;

        public UserEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<AppUser>> GetAll()
        {
            using (var response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<AppUser>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            using (var response = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddRole(string userId, string role)
        {
            var data = new { userId, role };
            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/AddRole", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveRole(string userId, string role)
        {
            var data = new { userId, role };
            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/RemoveRole", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
