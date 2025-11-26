using System;
using System.ComponentModel.DataAnnotations;

namespace GaraWeb.Models
{
    public class LienHe
    {
        public int MaLienHe { get; set; }

        [Required]
        public string HoTen { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string SoDienThoai { get; set; } = "";
        public string? ChuDe { get; set; }
        [Required]
        public string NoiDung { get; set; } = "";
        public DateTime NgayGui { get; set; }
        public bool DaXuLy { get; set; }
        public string? Username { get; set; } // Username của user đã liên hệ
    }
}
