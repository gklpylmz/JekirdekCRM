using JekirdekCRM.DTO.CustomerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JekirdekCRM.WEBUI.Pages.Customer
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<GetCustomerDto> Customers { get; set; } = new();
        [BindProperty(SupportsGet =true)]
        public GetFilterCustomerRequestDto FilterCustomerRequest { get; set; } = new();


        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient();
            if (FilterCustomerRequest.FirstName == null && FilterCustomerRequest.Region == null)
            {
                var responseMessage = await client.GetAsync("https://localhost:7278/api/Customers");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<GetCustomerDto>>(jsonData);
                }
            }
            if (FilterCustomerRequest.FirstName != null || FilterCustomerRequest.Region != null)
            {
                var responseMessageFilter = await client.GetAsync($"https://localhost:7278/api/Customers/GetCustomerByFirstNameAndRegion?firstName={FilterCustomerRequest.FirstName}&region={FilterCustomerRequest.Region}");
                if (responseMessageFilter.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessageFilter.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<List<GetCustomerDto>>(jsonData);
                }
            }     
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync($"https://localhost:7278/api/Customers/soft/{id}");

            if (!response.IsSuccessStatusCode)
            {

                await OnGet(); 
                return Page();
            }

            return RedirectToPage();
        }
    }
}
