using GaraWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GaraWeb.Controllers
{
    public class LienHeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LienHeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var thongTinLienHe = new ThongTinLienHe();
            return View(thongTinLienHe);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(string ten, string email, string soDienThoai, string chuDe, string noiDung)
        {
            try
            {
                // Gọi API để lưu liên hệ
                var apiUrl = _configuration["ApiSettings:BaseUrl"];
                using var httpClient = new HttpClient();
                
                var lienHe = new
                {
                    hoTen = ten,
                    email = email,
                    soDienThoai = soDienThoai,
                    chuDe = chuDe,
                    noiDung = noiDung,
                    username = User.Identity?.Name
                };
                
                var json = System.Text.Json.JsonSerializer.Serialize(lienHe);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync($"{apiUrl}/LienHe", content);
                
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cảm ơn bạn, phản hồi đã được gửi thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi liên hệ. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }
            
            return RedirectToAction("Index");
        }
    }
}
