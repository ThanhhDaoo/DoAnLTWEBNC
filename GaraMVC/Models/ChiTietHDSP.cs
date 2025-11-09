using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class ChiTietHDSP
    {
        public int MaCTSP { get; set; }
        public int MaHD { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public HoaDon? HoaDon { get; set; }
        public SanPham? SanPham { get; set; }
    }
}