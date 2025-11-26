using GaraMVC.ViewModels;

namespace GaraMVC.Services
{
    public interface IThongKeService
    {
        Task<TongQuanViewModel> GetTongQuanAsync();
        Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync();
        Task<DoanhThuTheoNgayResponse> GetDoanhThuTheoNgayAsync(DateTime fromDate, DateTime toDate);
        Task<List<TopDichVuDto>> GetTopDichVuAsync(int top = 5);
        Task<List<TopSanPhamDto>> GetTopSanPhamAsync(int top = 5);
        Task<List<HoaDonGanDayDto>> GetHoaDonGanDayAsync(int soLuong = 10);
        Task<List<TopKhachHangDto>> GetTopKhachHangAsync(int top = 5);
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}