using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace GaraMVC.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IConfiguration _configuration;

        public TestController(ILogger<TestController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestLogin(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            
            ViewBag.Username = username;
            ViewBag.Password = password;
            ViewBag.HashedPassword = hashedPassword;
            ViewBag.ExpectedHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";
            ViewBag.HashMatch = hashedPassword == "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";
            
            // Test API call
            var apiUrl = _configuration["ApiSettings:BaseUrl"];
            ViewBag.ApiUrl = apiUrl;
            
            try
            {
                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(apiUrl ?? "https://localhost:7002/api");
                
                var loginRequest = new
                {
                    username = username,
                    password = hashedPassword
                };
                
                var response = await httpClient.PostAsJsonAsync("/api/Auth/login", loginRequest);
                ViewBag.StatusCode = response.StatusCode;
                
                var content = await response.Content.ReadAsStringAsync();
                ViewBag.ResponseContent = content;
                ViewBag.Success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Success = false;
            }
            
            return View("Index");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
