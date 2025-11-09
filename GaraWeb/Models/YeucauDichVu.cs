namespace GaraWeb.Models
{
    public class YeucauDichVu
    {
        public int MaYeuCau { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public int MaDV { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayYeuCau { get; set; }
        public string TrangThai { get; set; } = "Mới";
        public DichVu? DichVu { get; set; }
        public DateTime? NgayHen { get; set; } // Ngày hẹn khách chọn
        public string? GioHen { get; set; } // Giờ hẹn khách chọn
    }
}

