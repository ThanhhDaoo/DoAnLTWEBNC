using System.Text.Json;
using GaraMVC.Models;
using GaraMVC.ViewModels;

namespace GaraMVC.Services
{
    public class ThongKeService : IThongKeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ThongKeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] + "/ThongKe";
        }

        public async Task<TongQuanViewModel> GetTongQuanAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/TongQuan");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var tongQuanDto = JsonSerializer.Deserialize<TongQuanDto>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return new TongQuanViewModel
                {
                    TongKhachHang = tongQuanDto?.TongKhachHang ?? 0,
                    TongXe = tongQuanDto?.TongXe ?? 0,
                    TongHoaDon = tongQuanDto?.TongHoaDon ?? 0,
                    TongDichVu = tongQuanDto?.TongDichVu ?? 0,
                    TongSanPham = tongQuanDto?.TongSanPham ?? 0,
                    DoanhThuHomNay = tongQuanDto?.DoanhThuHomNay ?? 0,
                    DoanhThuThang = tongQuanDto?.DoanhThuThang ?? 0,
                    DoanhThuThangNay = tongQuanDto?.DoanhThuThang ?? 0,
                    DoanhThuNam = tongQuanDto?.DoanhThuNam ?? 0,
                    SoHoaDonHomNay = tongQuanDto?.SoHoaDonHomNay ?? 0,
                    SoHoaDonThangNay = tongQuanDto?.SoHoaDonThang ?? 0,
                    SanPhamSapHet = tongQuanDto?.SanPhamSapHet ?? 0
                };
            }
            catch
            {
                return new TongQuanViewModel();
            }
        }

        public async Task<List<DoanhThuThangDto>> GetDoanhThuTheoThangAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/DoanhThuTheoThang");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DoanhThuThangDto>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DoanhThuThangDto>();
            }
            catch
            {
                return new List<DoanhThuThangDto>();
            }
        }

        public async Task<DoanhThuTheoNgayResponse> GetDoanhThuTheoNgayAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/DoanhThuTheoNgay?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DoanhThuTheoNgayResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new DoanhThuTheoNgayResponse();
            }
            catch
            {
                return new DoanhThuTheoNgayResponse();
            }
        }

        public async Task<List<TopDichVuDto>> GetTopDichVuAsync(int top = 5)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/TopDichVu?top={top}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TopDichVuDto>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TopDichVuDto>();
            }
            catch
            {
                return new List<TopDichVuDto>();
            }
        }

        public async Task<List<TopSanPhamDto>> GetTopSanPhamAsync(int top = 5)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/TopSanPham?top={top}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TopSanPhamDto>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TopSanPhamDto>();
            }
            catch
            {
                return new List<TopSanPhamDto>();
            }
        }

        public async Task<List<HoaDonGanDayDto>> GetHoaDonGanDayAsync(int soLuong = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/HoaDonGanDay?soLuong={soLuong}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<HoaDonGanDayDto>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<HoaDonGanDayDto>();
            }
            catch
            {
                return new List<HoaDonGanDayDto>();
            }
        }

        public async Task<List<TopKhachHangDto>> GetTopKhachHangAsync(int top = 5)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/TopKhachHang?top={top}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TopKhachHangDto>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TopKhachHangDto>();
            }
            catch
            {
                return new List<TopKhachHangDto>();
            }
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var dashboard = new DashboardViewModel();

            try
            {
                // Lấy tổng quan
                var tongQuan = await GetTongQuanAsync();
                dashboard.TongKhachHang = tongQuan.TongKhachHang;
                dashboard.TongXe = tongQuan.TongXe;
                dashboard.TongHoaDon = tongQuan.TongHoaDon;
                dashboard.TongDichVu = tongQuan.TongDichVu;
                dashboard.TongSanPham = tongQuan.TongSanPham;
                dashboard.DoanhThuHomNay = tongQuan.DoanhThuHomNay;
                dashboard.DoanhThuThangNay = tongQuan.DoanhThuThang;
                dashboard.DoanhThuNam = tongQuan.DoanhThuNam;
                dashboard.SoHoaDonHomNay = tongQuan.SoHoaDonHomNay;
                dashboard.SoHoaDonThangNay = tongQuan.SoHoaDonThangNay;
                dashboard.SanPhamSapHet = tongQuan.SanPhamSapHet;

                // Lấy dữ liệu charts song song
                var doanhThuTask = GetDoanhThuTheoThangAsync();
                var topDichVuTask = GetTopDichVuAsync(5);
                var topSanPhamTask = GetTopSanPhamAsync(5);
                var hoaDonTask = GetHoaDonGanDayAsync(5);
                var topKhachHangTask = GetTopKhachHangAsync(5);

                await Task.WhenAll(doanhThuTask, topDichVuTask, topSanPhamTask, hoaDonTask, topKhachHangTask);

                dashboard.DoanhThuTheoThang = await doanhThuTask;
                dashboard.TopDichVu = await topDichVuTask;
                dashboard.TopSanPham = await topSanPhamTask;
                dashboard.HoaDonGanDay = await hoaDonTask;
                dashboard.TopKhachHang = await topKhachHangTask;
            }
            catch
            {
                // Trả về dashboard rỗng nếu có lỗi
            }

            return dashboard;
        }
    }

    // DTOs cho API response
    public class TongQuanDto
    {
        public int TongKhachHang { get; set; }
        public int TongXe { get; set; }
        public int TongHoaDon { get; set; }
        public int TongDichVu { get; set; }
        public int TongSanPham { get; set; }
        public decimal DoanhThuHomNay { get; set; }
        public decimal DoanhThuThang { get; set; }
        public decimal DoanhThuNam { get; set; }
        public int SoHoaDonHomNay { get; set; }
        public int SoHoaDonThang { get; set; }
        public int SanPhamSapHet { get; set; }
    }

    public class DoanhThuThangDto
    {
        public int Thang { get; set; }
        public decimal DoanhThu { get; set; }
        public int SoHoaDon { get; set; }
    }

    public class TopDichVuDto
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class TopSanPhamDto
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class HoaDonGanDayDto
    {
        public int MaHD { get; set; }
        public string TenKH { get; set; } = string.Empty;
        public string BienSo { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string? TrangThai { get; set; }
    }

    public class TopKhachHangDto
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; } = string.Empty;
        public string? SDT { get; set; }
        public int SoLanSuDung { get; set; }
        public decimal TongChiTieu { get; set; }
    }

    public class DoanhThuTheoNgayResponse
    {
        public decimal TongDoanhThu { get; set; }
        public int TongHoaDon { get; set; }
        public List<DoanhThuNgayDto> ChiTiet { get; set; } = new();
    }

    public class DoanhThuNgayDto
    {
        public DateTime Ngay { get; set; }
        public decimal TongTien { get; set; }
        public int SoHoaDon { get; set; }
    }
}
