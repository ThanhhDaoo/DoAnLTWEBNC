namespace GaraWeb.Models
{
    public class DichVu
    {
        public int Id { get; set; }
        public string TenDichVu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public decimal Gia { get; set; }
        public string? HinhAnh { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
