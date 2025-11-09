using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class ChiTietHDDV
    {
        public int MaCTDV { get; set; }
        public int MaHD { get; set; }
        public int MaDV { get; set; }
        public int SoLuong { get; set; } = 1;
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public HoaDon? HoaDon { get; set; }
        public DichVu? DichVu { get; set; }
    }
}