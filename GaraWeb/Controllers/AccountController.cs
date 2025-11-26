using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GaraWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public class LoginViewModel
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? ReturnUrl { get; set; }
        }

        public class RegisterViewModel
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"]; // e.g. https://localhost:7001/api
                
                // Hash password với SHA256
                var hashedPassword = HashPassword(model.Password);
                
                var payload = JsonSerializer.Serialize(new { username = model.Username, password = hashedPassword });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{baseUrl}/Auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra thông tin.");
                    return View(model);
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var username = root.GetProperty("username").GetString() ?? model.Username;
                var role = root.TryGetProperty("role", out var roleProp) ? (roleProp.GetString() ?? "Customer") : "Customer";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi kết nối: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return View(model);
            }
            
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"]; // e.g. https://localhost:7001/api
                
                // Gửi plain password cho register, API sẽ tự hash
                var payload = JsonSerializer.Serialize(new { username = model.Username, password = model.Password, email = model.Email });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{baseUrl}/Auth/register", content);

                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(msg) ? "Đăng ký thất bại." : msg);
                    return View(model);
                }

                TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi kết nối: {ex.Message}");
                return View(model);
            }
        }
        
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult TestLogin()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyRequests()
        {
            try
            {
                var username = User.Identity?.Name;
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login");
                }

                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.GetAsync($"{baseUrl}/Yeucau");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var allRequests = JsonSerializer.Deserialize<List<Models.YeucauDichVu>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Models.YeucauDichVu>();

                    // Filter theo username của user hiện tại
                    var myRequests = allRequests.Where(r => r.Username == username).ToList();
                    return View(myRequests);
                }

                return View(new List<Models.YeucauDichVu>());
            }
            catch
            {
                return View(new List<Models.YeucauDichVu>());
            }
        }

        [Authorize]
        public async Task<IActionResult> MyContacts()
        {
            try
            {
                var username = User.Identity?.Name;
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login");
                }

                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = await _httpClient.GetAsync($"{baseUrl}/LienHe");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var allContacts = JsonSerializer.Deserialize<List<Models.LienHe>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Models.LienHe>();

                    // Filter theo username của user hiện tại
                    var myContacts = allContacts.Where(c => c.Username == username).ToList();
                    return View(myContacts);
                }

                return View(new List<Models.LienHe>());
            }
            catch
            {
                return View(new List<Models.LienHe>());
            }
        }
    }
}


