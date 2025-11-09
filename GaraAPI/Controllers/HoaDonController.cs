using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaraAPI.Data;
using GaraAPI.Models;

namespace GaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly GaraDbContext _context;

        public HoaDonController(GaraDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDon>>> GetHoaDons()
        {
            return await _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.Xe)
                .Include(h => h.User)
                .OrderByDescending(h => h.NgayLap)
                .ToListAsync();
        }

        // ✅ SỬA LẠI GetHoaDon
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDon>> GetHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.Xe)
                .Include(h => h.User)
                .Include(h => h.ChiTietDichVus!) // ✅ Thêm !
                    .ThenInclude(ct => ct.DichVu)
                .Include(h => h.ChiTietSanPhams!) // ✅ Thêm !
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hoaDon == null)
                return NotFound();

            return hoaDon;
        }

        [HttpPost]
        public async Task<ActionResult<HoaDon>> PostHoaDon(HoaDonCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var hoaDon = new HoaDon
                {
                    MaKH = dto.MaKH,
                    MaXe = dto.MaXe,
                    UserId = dto.UserId,
                    NgayLap = DateTime.Now,
                    HinhThucTT = dto.HinhThucTT,
                    TrangThai = "Đang xử lý"
                };

                _context.HoaDons.Add(hoaDon);
                await _context.SaveChangesAsync();

                decimal tongTien = 0;

                if (dto.DichVus != null && dto.DichVus.Any())
                {
                    foreach (var dv in dto.DichVus)
                    {
                        var dichVu = await _context.DichVus.FindAsync(dv.MaDV);
                        if (dichVu == null)
                            throw new Exception($"Dịch vụ {dv.MaDV} không tồn tại");

                        var chiTiet = new ChiTietHDDV
                        {
                            MaHD = hoaDon.MaHD,
                            MaDV = dv.MaDV,
                            SoLuong = dv.SoLuong,
                            DonGia = dichVu.Gia
                        };

                        _context.ChiTietHDDVs.Add(chiTiet);
                        tongTien += dichVu.Gia * dv.SoLuong;
                    }
                }

                if (dto.SanPhams != null && dto.SanPhams.Any())
                {
                    foreach (var sp in dto.SanPhams)
                    {
                        var sanPham = await _context.SanPhams.FindAsync(sp.MaSP);
                        if (sanPham == null)
                            throw new Exception($"Sản phẩm {sp.MaSP} không tồn tại");

                        if (sanPham.SoLuongTon < sp.SoLuong)
                            throw new Exception($"Sản phẩm {sanPham.TenSanPham} không đủ tồn kho");

                        var chiTiet = new ChiTietHDSP
                        {
                            MaHD = hoaDon.MaHD,
                            MaSP = sp.MaSP,
                            SoLuong = sp.SoLuong,
                            DonGia = sanPham.Gia
                        };

                        _context.ChiTietHDSPs.Add(chiTiet);
                        tongTien += sanPham.Gia * sp.SoLuong;

                        sanPham.SoLuongTon -= sp.SoLuong;
                    }
                }

                hoaDon.TongTien = tongTien;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetHoaDon), new { id = hoaDon.MaHD }, hoaDon);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Complete")]
        public async Task<IActionResult> CompleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
                return NotFound();

            hoaDon.TrangThai = "Hoàn thành";
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.ChiTietDichVus)
                .Include(h => h.ChiTietSanPhams)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hoaDon == null)
                return NotFound();

            // ✅ Thêm null check
            if (hoaDon.ChiTietSanPhams != null)
            {
                foreach (var ct in hoaDon.ChiTietSanPhams)
                {
                    var sanPham = await _context.SanPhams.FindAsync(ct.MaSP);
                    if (sanPham != null)
                        sanPham.SoLuongTon += ct.SoLuong;
                }
            }

            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class HoaDonCreateDto
    {
        public int MaKH { get; set; }
        public int MaXe { get; set; }
        public int UserId { get; set; }
        public string? HinhThucTT { get; set; }
        public List<DichVuDto>? DichVus { get; set; }
        public List<SanPhamDto>? SanPhams { get; set; }
    }

    public class DichVuDto
    {
        public int MaDV { get; set; }
        public int SoLuong { get; set; }
    }

    public class SanPhamDto
    {
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
    }
}