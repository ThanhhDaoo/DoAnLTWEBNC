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
        public IActionResult Index(string ten, string email, string soDienThoai, string chuDe, string noiDung)
        {
            // Lưu liên hệ vào database
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO LIENHE (HoTen, Email, SoDienThoai, ChuDe, NoiDung) VALUES (@HoTen, @Email, @SoDienThoai, @ChuDe, @NoiDung)", conn);
                cmd.Parameters.AddWithValue("@HoTen", ten);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@ChuDe", (object?)chuDe ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                cmd.ExecuteNonQuery();
            }
            TempData["SuccessMessage"] = "Cảm ơn bạn, phản hồi đã được gửi thành công!";
            return RedirectToAction("Index");
        }
    }
}
