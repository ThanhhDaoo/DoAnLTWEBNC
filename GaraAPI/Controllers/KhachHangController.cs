using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaraAPI.Data;
using GaraAPI.Models;

namespace GaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly GaraDbContext _context;

        public KhachHangController(GaraDbContext context)
        {
            _context = context;
        }

        // GET: api/KhachHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
            return await _context.KhachHangs.Include(k => k.DanhSachXe).ToListAsync();
        }

        // GET: api/KhachHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
            var khachHang = await _context.KhachHangs
                .Include(k => k.DanhSachXe)
                .FirstOrDefaultAsync(k => k.MaKH == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // GET: api/KhachHang/sdt/{sdt}
        [HttpGet("sdt/{sdt}")]
        public async Task<ActionResult<KhachHang>> GetKhachHangBySDT(string sdt)
        {
            var khachHang = await _context.KhachHangs
                .Include(k => k.DanhSachXe)
                .OrderByDescending(k => k.MaKH)
                .FirstOrDefaultAsync(k => k.SDT == sdt);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // POST: api/KhachHang
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            // Ensure NgayDangKy is set if not provided
            if (khachHang.NgayDangKy == default(DateTime))
            {
                khachHang.NgayDangKy = DateTime.Now;
            }

            // Ensure UserId is null if 0
            if (khachHang.UserId == 0)
            {
                khachHang.UserId = null;
            }

            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKhachHang), new { id = khachHang.MaKH }, khachHang);
        }

        // PUT: api/KhachHang/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.MaKH)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/KhachHang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.MaKH == id);
        }
    }
}