namespace GaraWeb.Models
{
    public class GioHangItem
    {
        public int SanPhamId { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string HinhAnh { get; set; } = string.Empty;
        public decimal ThanhTien => Gia * SoLuong;
    }
}
