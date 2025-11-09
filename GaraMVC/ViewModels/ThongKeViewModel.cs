using GaraMVC.Services;

namespace GaraMVC.ViewModels
{
    public class ThongKeViewModel
    {
        public TongQuanViewModel TongQuan { get; set; } = new TongQuanViewModel();
        public List<DoanhThuThangDto> DoanhThuTheoThang { get; set; } = new List<DoanhThuThangDto>();
        public List<TopDichVuDto> TopDichVu { get; set; } = new List<TopDichVuDto>();
        public List<TopSanPhamDto> TopSanPham { get; set; } = new List<TopSanPhamDto>();
        public List<HoaDonGanDayDto> HoaDonGanDay { get; set; } = new List<HoaDonGanDayDto>();
    }
}
