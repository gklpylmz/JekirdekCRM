using JekirdekCRM.DTO.CustomerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JekirdekCRM.WEBUI.Pages.Customer
{
    [Authorize(Roles = "Admin")]
    public class AddCustomerModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddCustomerModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CreateCustomerDto Customer { get; set; } = new();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(Customer);
            var content = new StringContent(jsonData,Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7278/api/Customers",content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
