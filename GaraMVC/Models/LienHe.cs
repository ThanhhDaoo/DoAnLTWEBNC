using System;

namespace GaraMVC.Models
{
    public class LienHe
    {
        public int MaLienHe { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string ChuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGui { get; set; }
        public bool DaXuLy { get; set; }
    }
}