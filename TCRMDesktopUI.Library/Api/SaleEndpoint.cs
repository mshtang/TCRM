using System.Net.Http;
using System.Threading.Tasks;
using TCRMDesktopUI.Library.Models;

namespace TCRMDesktopUI.Library.Api
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private IAPIHelper _apiHelper;

        public SaleEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSale(Sale sale)
        {
            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
