namespace GaraWeb.Models
{
    public class SanPham
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal Gia { get; set; }
        public string? HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
