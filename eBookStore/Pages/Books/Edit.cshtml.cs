using System.Collections.Generic;
using System.Linq;
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
using Microsoft.EntityFrameworkCore;

namespace eBookStore.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public EditModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
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
            
            var responseP = await apiClient.GetAsync("Publishers");
            var dataStringP = await responseP.Content.ReadAsStringAsync();
            var publishers = JsonSerializer.Deserialize<IEnumerable<Publisher>>(dataStringP, jsonOption);
            ViewData["PubId"] = new SelectList(publishers, "PubId", "PublisherName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await apiClient.PutAsJsonAsync($"Books/{Book.BookId}", Book);

            return RedirectToPage("./Index");
        }
    }
}
