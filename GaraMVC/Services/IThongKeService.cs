using GaraMVC.ViewModels;

namespace GaraMVC.Services
{
    public interface IThongKeService
    {
        Task<TongQuanViewModel> GetTongQuanAsync();
        Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync();
        Task<List<TopDichVuDto>> GetTopDichVuAsync(int top = 5);
        Task<List<TopSanPhamDto>> GetTopSanPhamAsync(int top = 5);
        Task<List<HoaDonGanDayDto>> GetHoaDonGanDayAsync(int soLuong = 10);
    }
}