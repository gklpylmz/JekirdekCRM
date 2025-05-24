using JekirdekCRM.DTO.CustomerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JekirdekCRM.WEBUI.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public CustomerStatisticDto CustomerStatisticDto { get; set; }
        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7278/api/Customers/GetCustomerStatistics");

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                CustomerStatisticDto = JsonConvert.DeserializeObject<CustomerStatisticDto>(json);
            }
        }
    }
}
