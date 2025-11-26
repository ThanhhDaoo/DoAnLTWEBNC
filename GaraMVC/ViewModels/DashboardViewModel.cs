using GaraMVC.Services;

namespace GaraMVC.ViewModels
{
    public class DashboardViewModel
    {
        // Tổng quan
        public int TongKhachHang { get; set; }
        public int TongXe { get; set; }
        public int TongHoaDon { get; set; }
        public int TongDichVu { get; set; }
        public int TongSanPham { get; set; }
        public int SanPhamSapHet { get; set; }
        
        // Doanh thu
        public decimal DoanhThuHomNay { get; set; }
        public decimal DoanhThuThangNay { get; set; }
        public decimal DoanhThuNam { get; set; }
        public int SoHoaDonHomNay { get; set; }
        public int SoHoaDonThangNay { get; set; }
        
        // Dữ liệu cho charts
        public List<DoanhThuThangDto> DoanhThuTheoThang { get; set; } = new();
        public List<TopDichVuDto> TopDichVu { get; set; } = new();
        public List<TopSanPhamDto> TopSanPham { get; set; } = new();
        public List<HoaDonGanDayDto> HoaDonGanDay { get; set; } = new();
        public List<TopKhachHangDto> TopKhachHang { get; set; } = new();
        
        // Tính toán phần trăm mục tiêu (ví dụ: mục tiêu 100 hóa đơn/tháng)
        public int PhanTramMucTieu => TongHoaDon > 0 ? Math.Min((SoHoaDonThangNay * 100) / Math.Max(TongHoaDon, 1), 100) : 0;
    }
}