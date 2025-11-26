using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaraAPI.Data;
using GaraAPI.Models;

namespace GaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly GaraDbContext _context;

        public ThongKeController(GaraDbContext context)
        {
            _context = context;
        }

        // GET: api/ThongKe/TongQuan
        [HttpGet("TongQuan")]
        public async Task<ActionResult<TongQuanDto>> GetTongQuan()
        {
            var today = DateTime.Today;
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var tongQuan = new TongQuanDto
            {
                TongKhachHang = await _context.KhachHangs.CountAsync(),
                TongXe = await _context.Xes.CountAsync(),
                TongHoaDon = await _context.HoaDons.CountAsync(),
                TongDichVu = await _context.DichVus.CountAsync(),
                TongSanPham = await _context.SanPhams.CountAsync(),
                
                // Doanh thu hôm nay
                DoanhThuHomNay = await _context.HoaDons
                    .Where(h => h.NgayLap.Date == today && h.TrangThai == "Hoàn thành")
                    .SumAsync(h => h.TongTien),
                
                // Doanh thu tháng này
                DoanhThuThang = await _context.HoaDons
                    .Where(h => h.NgayLap.Month == currentMonth && h.NgayLap.Year == currentYear && h.TrangThai == "Hoàn thành")
                    .SumAsync(h => h.TongTien),
                
                // Doanh thu năm
                DoanhThuNam = await _context.HoaDons
                    .Where(h => h.NgayLap.Year == currentYear && h.TrangThai == "Hoàn thành")
                    .SumAsync(h => h.TongTien),
                
                // Số hóa đơn hôm nay
                SoHoaDonHomNay = await _context.HoaDons
                    .Where(h => h.NgayLap.Date == today)
                    .CountAsync(),
                
                // Số hóa đơn tháng này
                SoHoaDonThang = await _context.HoaDons
                    .Where(h => h.NgayLap.Month == currentMonth && h.NgayLap.Year == currentYear)
                    .CountAsync(),
                
                // Sản phẩm sắp hết (tồn kho < 10)
                SanPhamSapHet = await _context.SanPhams
                    .Where(sp => sp.SoLuongTon < 10 && sp.SoLuongTon > 0)
                    .CountAsync()
            };

            return tongQuan;
        }

        // GET: api/ThongKe/DoanhThuTheoThang
        [HttpGet("DoanhThuTheoThang")]
        public async Task<ActionResult<List<DoanhThuThangDto>>> GetDoanhThuTheoThang()
        {
            var doanhThu = await _context.HoaDons
                .Where(h => h.NgayLap.Year == DateTime.Now.Year)
                .GroupBy(h => h.NgayLap.Month)
                .Select(g => new DoanhThuThangDto
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(h => h.TongTien),
                    SoHoaDon = g.Count()
                })
                .OrderBy(d => d.Thang)
                .ToListAsync();

            return doanhThu;
        }

        // GET: api/ThongKe/DoanhThuTheoNgay
        [HttpGet("DoanhThuTheoNgay")]
        public async Task<ActionResult<DoanhThuTheoNgayResponse>> GetDoanhThuTheoNgay(DateTime? fromDate, DateTime? toDate)
        {
            var from = fromDate ?? DateTime.Now.AddMonths(-1);
            var to = toDate ?? DateTime.Now;

            var hoaDons = await _context.HoaDons
                .Where(h => h.NgayLap >= from && h.NgayLap <= to && h.TrangThai == "Hoàn thành")
                .ToListAsync();

            var chiTiet = hoaDons
                .GroupBy(h => h.NgayLap.Date)
                .Select(g => new DoanhThuNgayDto
                {
                    Ngay = g.Key,
                    TongTien = g.Sum(h => h.TongTien),
                    SoHoaDon = g.Count()
                })
                .OrderBy(d => d.Ngay)
                .ToList();

            return new DoanhThuTheoNgayResponse
            {
                TongDoanhThu = hoaDons.Sum(h => h.TongTien),
                TongHoaDon = hoaDons.Count,
                ChiTiet = chiTiet
            };
        }

        // GET: api/ThongKe/TopDichVu
        [HttpGet("TopDichVu")]
        public async Task<ActionResult<List<TopDichVuDto>>> GetTopDichVu(int top = 5)
        {
            var topDichVu = await _context.ChiTietHDDVs
                .Include(ct => ct.DichVu)
                .GroupBy(ct => new { ct.MaDV, ct.DichVu!.TenDichVu })
                .Select(g => new TopDichVuDto
                {
                    MaDV = g.Key.MaDV,
                    TenDV = g.Key.TenDichVu,
                    SoLuong = g.Sum(ct => ct.SoLuong),
                    DoanhThu = g.Sum(ct => ct.ThanhTien)
                })
                .OrderByDescending(d => d.SoLuong)
                .Take(top)
                .ToListAsync();

            return topDichVu;
        }

        // GET: api/ThongKe/TopSanPham
        [HttpGet("TopSanPham")]
        public async Task<ActionResult<List<TopSanPhamDto>>> GetTopSanPham(int top = 5)
        {
            var topSanPham = await _context.ChiTietHDSPs
                .Include(ct => ct.SanPham)
                .GroupBy(ct => new { ct.MaSP, ct.SanPham!.TenSanPham })
                .Select(g => new TopSanPhamDto
                {
                    MaSP = g.Key.MaSP,
                    TenSP = g.Key.TenSanPham,
                    SoLuong = g.Sum(ct => ct.SoLuong),
                    DoanhThu = g.Sum(ct => ct.ThanhTien)
                })
                .OrderByDescending(s => s.SoLuong)
                .Take(top)
                .ToListAsync();

            return topSanPham;
        }

        // GET: api/ThongKe/HoaDonGanDay
        [HttpGet("HoaDonGanDay")]
        public async Task<ActionResult<List<HoaDonGanDayDto>>> GetHoaDonGanDay(int soLuong = 10)
        {
            var hoaDonGanDay = await _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.Xe)
                .OrderByDescending(h => h.NgayLap)
                .Take(soLuong)
                .Select(h => new HoaDonGanDayDto
                {
                    MaHD = h.MaHD,
                    TenKH = h.KhachHang!.TenKH,
                    BienSo = h.Xe!.BienSo,
                    NgayLap = h.NgayLap,
                    TongTien = h.TongTien,
                    TrangThai = h.TrangThai
                })
                .ToListAsync();

            return hoaDonGanDay;
        }

        // GET: api/ThongKe/TopKhachHang
        [HttpGet("TopKhachHang")]
        public async Task<ActionResult<List<TopKhachHangDto>>> GetTopKhachHang(int top = 10)
        {
            var topKhachHang = await _context.HoaDons
                .Include(h => h.KhachHang)
                .Where(h => h.TrangThai == "Hoàn thành")
                .GroupBy(h => new { h.MaKH, h.KhachHang!.TenKH, h.KhachHang.SDT })
                .Select(g => new TopKhachHangDto
                {
                    MaKH = g.Key.MaKH,
                    TenKH = g.Key.TenKH,
                    SDT = g.Key.SDT,
                    SoLanSuDung = g.Count(),
                    TongChiTieu = g.Sum(h => h.TongTien)
                })
                .OrderByDescending(k => k.TongChiTieu)
                .Take(top)
                .ToListAsync();

            return topKhachHang;
        }
    }

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