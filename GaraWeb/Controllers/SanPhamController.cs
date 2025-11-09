using GaraWeb.Models;
using GaraWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GaraWeb.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly IApiService _apiService;
        private const string GioHangSessionKey = "GioHang";

        public SanPhamController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var sanPhams = await _apiService.GetSanPhamsAsync();
            return View(sanPhams);
        }

        [Authorize]
        public async Task<IActionResult> ChiTiet(int id)
        {
            var sanPham = await _apiService.GetSanPhamByIdAsync(id);
            
            if (sanPham == null || sanPham.Id == 0)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ThemVaoGioHang(int sanPhamId, int soLuong = 1)
        {
            var sanPham = _apiService.GetSanPhamByIdAsync(sanPhamId).Result;
            
            if (sanPham == null || sanPham.Id == 0)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });
            }

            var gioHang = GetGioHang();
            var item = gioHang.FirstOrDefault(x => x.SanPhamId == sanPhamId);
            
            if (item != null)
            {
                item.SoLuong += soLuong;
            }
            else
            {
                gioHang.Add(new GioHangItem
                {
                    SanPhamId = sanPham.Id,
                    TenSanPham = sanPham.TenSanPham,
                    Gia = sanPham.Gia,
                    SoLuong = soLuong,
                    HinhAnh = sanPham.HinhAnh
                });
            }

            SaveGioHang(gioHang);
            
            return Json(new { success = true, message = "Đã thêm vào giỏ hàng", count = gioHang.Sum(x => x.SoLuong) });
        }

        [Authorize]
        public IActionResult GioHang()
        {
            var gioHang = GetGioHang();
            return View(gioHang);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CapNhatGioHang(int sanPhamId, int soLuong)
        {
            var gioHang = GetGioHang();
            var item = gioHang.FirstOrDefault(x => x.SanPhamId == sanPhamId);
            
            if (item != null)
            {
                if (soLuong <= 0)
                {
                    gioHang.Remove(item);
                }
                else
                {
                    item.SoLuong = soLuong;
                }
                
                SaveGioHang(gioHang);
            }

            return RedirectToAction("GioHang");
        }

        [HttpPost]
        [Authorize]
        public IActionResult XoaKhoiGioHang(int sanPhamId)
        {
            var gioHang = GetGioHang();
            var item = gioHang.FirstOrDefault(x => x.SanPhamId == sanPhamId);
            
            if (item != null)
            {
                gioHang.Remove(item);
                SaveGioHang(gioHang);
            }

            return RedirectToAction("GioHang");
        }

        private List<GioHangItem> GetGioHang()
        {
            var gioHangJson = HttpContext.Session.GetString(GioHangSessionKey);
            if (string.IsNullOrEmpty(gioHangJson))
            {
                return new List<GioHangItem>();
            }
            
            return JsonSerializer.Deserialize<List<GioHangItem>>(gioHangJson) ?? new List<GioHangItem>();
        }

        private void SaveGioHang(List<GioHangItem> gioHang)
        {
            var gioHangJson = JsonSerializer.Serialize(gioHang);
            HttpContext.Session.SetString(GioHangSessionKey, gioHangJson);
        }

        public IActionResult GetCartCount()
        {
            var gioHang = GetGioHang();
            var count = gioHang.Sum(x => x.SoLuong);
            return Json(count);
        }
    }
}
