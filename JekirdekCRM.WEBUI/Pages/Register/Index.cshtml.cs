using JekirdekCRM.DTO.AuthDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace JekirdekCRM.WEBUI.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RegisterDto RegisterDto { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(RegisterDto);
            var content = new StringContent(jsonData,Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7278/api/Auth/register", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                ErrorMessage = "Kayýt olma baþarýsýz";
                return Page();
            }

            return RedirectToPage("/Login/Index");
        }
    }
}
