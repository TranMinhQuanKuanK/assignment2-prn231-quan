using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;

namespace eBookStore.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient apiClient;

        public LoginModel(HttpClient apiClient)
        {
            this.apiClient = apiClient;
        }
        
        [BindProperty]
        [Required]
        public string Email { get; set; }
        
        [BindProperty]
        [Required]
        public string Password { get; set; }
        
        public string Message { get; set; }
        
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login/Login");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var response = await apiClient.PostAsJsonAsync("Login", new LoginCreateModel()
            {
                Email = this.Email,
                Password = this.Password,
            });
            var dataString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(dataString, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });
            
            if (user == null)
            {
                Message = "Wrong email or password";
                ViewData["Message"] = Message;
                return Page();
            }

            if (user.RoleId == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("../Users/Details"); //TODO: dien url
            }

            if (user.RoleId == 2)
            {
                var claims = new List<Claim>
                { 
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return RedirectToPage("../Books/Index");
            }
            return Page();
        }
    }
}