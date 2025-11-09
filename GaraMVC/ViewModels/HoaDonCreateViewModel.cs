using GaraMVC.Models;
namespace GaraMVC.ViewModels
{
    public class HoaDonCreateViewModel
    {
        public int MaKH { get; set; }
        public int MaXe { get; set; }
        public int UserId { get; set; }
        public string? HinhThucTT { get; set; }

        public List<DichVuItemViewModel>? DichVuItems { get; set; } = new();
        public List<SanPhamItemViewModel>? SanPhamItems { get; set; } = new();

        // For dropdowns
        public List<KhachHang> KhachHangs { get; set; } = new();
        public List<Xe> Xes { get; set; } = new();
        public List<DichVu> DichVus { get; set; } = new();
        public List<SanPham> SanPhams { get; set; } = new();
    }

    public class DichVuItemViewModel
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }

    public class SanPhamItemViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongTon { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }
}