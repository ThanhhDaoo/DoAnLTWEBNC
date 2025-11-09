using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GaraAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LienHeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LienHeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // GET: api/LienHe
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<object>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM LIENHE ORDER BY NgayGui DESC", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new {
                        MaLienHe = reader["MaLienHe"],
                        HoTen = reader["HoTen"],
                        Email = reader["Email"],
                        SoDienThoai = reader["SoDienThoai"],
                        ChuDe = reader["ChuDe"],
                        NoiDung = reader["NoiDung"],
                        NgayGui = reader["NgayGui"],
                        DaXuLy = reader["DaXuLy"]
                    });
                }
            }
            return Ok(list);
        }

        // PUT: api/LienHe/update-status
        [HttpPut("update-status")]
        public IActionResult UpdateStatus(int id, bool daxuly)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE LIENHE SET DaXuLy=@DaXuLy WHERE MaLienHe=@Id", conn);
                cmd.Parameters.AddWithValue("@DaXuLy", daxuly);
                cmd.Parameters.AddWithValue("@Id", id);
                var count = cmd.ExecuteNonQuery();
                return Ok(new { updated = count });
            }
        }
    }
}