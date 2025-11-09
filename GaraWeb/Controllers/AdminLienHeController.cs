using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GaraWeb.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GaraWeb.Controllers
{
    public class AdminLienHeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AdminLienHeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // Danh sách liên hệ
        public IActionResult Index()
        {
            List<LienHe> ds = new List<LienHe>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM LIENHE ORDER BY NgayGui DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new LienHe
                        {
                            MaLienHe = reader.GetInt32(reader.GetOrdinal("MaLienHe")),
                            HoTen = reader["HoTen"].ToString(),
                            Email = reader["Email"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString(),
                            ChuDe = reader["ChuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            NgayGui = (DateTime)reader["NgayGui"],
                            DaXuLy = (bool)reader["DaXuLy"]
                        });
                    }
                }
            }
            return View(ds);
        }

        // Đổi trạng thái Đã xử lý
        [HttpPost]
        public IActionResult UpdateStatus(int id, bool daxuly)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE LIENHE SET DaXuLy=@DaXuLy WHERE MaLienHe=@Id", conn);
                cmd.Parameters.AddWithValue("@DaXuLy", daxuly);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return Json(new { success = true });
        }
    }
}
