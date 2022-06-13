using System;
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

namespace eBookStore.Pages.Users
{
    [Authorize(Roles = "User")]
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
        public DataAccess.Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "User")
            {
                RedirectToPage("../Error");
            }
            ViewData.Add("IsUser", true);
            
            if (id == null)
            {
                return NotFound();
            }

            var response = await apiClient.GetAsync($"Users/{id}");
            var dataString = await response.Content.ReadAsStringAsync();
            User = JsonSerializer.Deserialize<User>(dataString, jsonOption);
            
            var responseP = await apiClient.GetAsync("Publishers");
            var dataStringP = await responseP.Content.ReadAsStringAsync();
            var publishers = JsonSerializer.Deserialize<IEnumerable<Publisher>>(dataStringP, jsonOption);
            
            var responseR = await apiClient.GetAsync("Roles");
            var dataStringR = await responseR.Content.ReadAsStringAsync();
            var roles = JsonSerializer.Deserialize<IEnumerable<Role>>(dataStringR, jsonOption);

            if (User == null)
            {
                return NotFound();
            }
           ViewData["PubId"] = new SelectList(publishers, "PubId", "PublisherName");
           ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleDescription");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Console.WriteLine(User.Source);
            var response = await apiClient.PutAsJsonAsync($"Users/{User.UserId}", User);
            Console.WriteLine(response.StatusCode);
            
            return RedirectToPage("./Details");
        }
    }
}
