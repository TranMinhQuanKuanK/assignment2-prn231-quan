using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBookStore.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public CreateModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Admin")
            {
                RedirectToPage("../Error");
            }
            
            var response = await apiClient.GetAsync("Publishers");
            var dataString = await response.Content.ReadAsStringAsync();
            var publishers = JsonSerializer.Deserialize<IEnumerable<Publisher>>(dataString, jsonOption);
            ViewData["PubId"] = new SelectList(publishers, "PubId", "PublisherName");
            return Page();
        }

        [BindProperty]
        public DataAccess.Models.Book Book { get; set; }

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await apiClient.PostAsJsonAsync("Books", Book);

            return RedirectToPage("./Index");
        }
    }
}
