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
            var tongQuan = new TongQuanDto
            {
                TongKhachHang = await _context.KhachHangs.CountAsync(),
                TongXe = await _context.Xes.CountAsync(),
                TongHoaDon = await _context.HoaDons.CountAsync(),
                TongDichVu = await _context.DichVus.CountAsync(),
                TongSanPham = await _context.SanPhams.CountAsync(),
                DoanhThuThang = await _context.HoaDons
                    .Where(h => h.NgayLap.Month == DateTime.Now.Month && h.NgayLap.Year == DateTime.Now.Year)
                    .SumAsync(h => h.TongTien),
                DoanhThuNam = await _context.HoaDons
                    .Where(h => h.NgayLap.Year == DateTime.Now.Year)
                    .SumAsync(h => h.TongTien)
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
    }

    public class TongQuanDto
    {
        public int TongKhachHang { get; set; }
        public int TongXe { get; set; }
        public int TongHoaDon { get; set; }
        public int TongDichVu { get; set; }
        public int TongSanPham { get; set; }
        public decimal DoanhThuThang { get; set; }
        public decimal DoanhThuNam { get; set; }
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
}