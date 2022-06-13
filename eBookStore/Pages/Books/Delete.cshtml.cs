using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eBookStore.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public DeleteModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        [BindProperty]
        public DataAccess.Models.Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                        if (role != "Admin")
                        {
                            RedirectToPage("../Error");
                        }
            
                        var response = await apiClient.GetAsync($"Books/{id}");
                        var dataString = await response.Content.ReadAsStringAsync();
                        Book = JsonSerializer.Deserialize<Book>(dataString, jsonOption);
                        
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await apiClient.DeleteAsync($"Books/{id}");

            return RedirectToPage("./Index");
        }
    }
}
