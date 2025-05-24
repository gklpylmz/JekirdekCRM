using JekirdekCRM.DTO.CustomerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace JekirdekCRM.WEBUI.Pages.Customer
{
    [Authorize(Roles = "Admin")]
    public class UpdateCustomerModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateCustomerModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [BindProperty]
        public UpdateCustomerDto UpdateCustomerDto { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7278/api/Customers/"+id);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            UpdateCustomerDto = JsonConvert.DeserializeObject<UpdateCustomerDto>(json);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(UpdateCustomerDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7278/api/Customers", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
