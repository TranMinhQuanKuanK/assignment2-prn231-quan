using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eBookStore.Pages.Users
{
    [Authorize(Roles = "User")]
    public class DetailsModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public DetailsModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public DataAccess.Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "User")
            {
                RedirectToPage("../Error");
            }
            ViewData.Add("IsUser", true);

            var idString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idString == null)
            {
                return NotFound();
            }

            var id = int.Parse(idString);

            var response = await apiClient.GetAsync($"Users/{id}");
            var dataString = await response.Content.ReadAsStringAsync();
            User = JsonSerializer.Deserialize<User>(dataString, jsonOption);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
