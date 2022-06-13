using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eBookStore.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HttpClient apiClient;
        private readonly JsonSerializerOptions jsonOption;

        public IndexModel(HttpClient apiClient, JsonSerializerOptions jsonOption)
        {
            this.apiClient = apiClient;
            this.jsonOption = jsonOption;
        }

        public IList<DataAccess.Models.Book> Book { get;set; }
        public string SearchName { get; set; }
        public int SearchPrice { get; set; }

        public async Task OnGetAsync(string searchName, int searchPrice)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Admin")
            {
                RedirectToPage("../Error");
            }

            var uri = "Books";
            if (String.IsNullOrEmpty(searchName))
            {
                searchName = "";
            }
            uri += $"?$filter=contains(Title, '{searchName}')";
            if (searchPrice != null && searchPrice > 0)
            {
                uri += $" and Price eq {searchPrice}";
            }

            SearchName = searchName;
            SearchPrice = searchPrice;
            
            Console.WriteLine(SearchName);
            Console.WriteLine(SearchPrice);

            var response = await apiClient.GetAsync(uri);
            var dataString = await response.Content.ReadAsStringAsync();
            Book = JsonSerializer.Deserialize<IEnumerable<Book>>(dataString, jsonOption).ToList();
        }
    }
}
