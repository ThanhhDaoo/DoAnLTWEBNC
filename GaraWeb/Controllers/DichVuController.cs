using GaraWeb.Models;
using GaraWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GaraWeb.Controllers
{
    public class DichVuController : Controller
    {
        private readonly IApiService _apiService;

        public DichVuController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var dichVus = await _apiService.GetDichVusAsync();
            return View(dichVus);
        }

        [Authorize]
        public async Task<IActionResult> DatDichVu(int id)
        {
            var dichVus = await _apiService.GetDichVusAsync();
            var dichVu = dichVus.FirstOrDefault(d => d.Id == id);
            
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DatDichVu(int id, string tenKhachHang, string soDienThoai, string diaChi, string ngayHen, string gioHen, string ghiChu)
        {
            try
            {
                DateTime? dHen = null;
                if (!string.IsNullOrEmpty(ngayHen))
                {
                    if(DateTime.TryParse(ngayHen, out var d))
                        dHen = d;
                }
                var yeucau = new YeucauDichVu
                {
                    MaDV = id,
                    TenKhachHang = tenKhachHang,
                    SoDienThoai = soDienThoai,
                    DiaChi = diaChi,
                    GhiChu = ghiChu,
                    NgayYeuCau = DateTime.Now,
                    TrangThai = "Mới",
                    NgayHen = dHen,
                    GioHen = gioHen
                };

                var result = await _apiService.CreateYeucauDichVuAsync(yeucau);
                if (result)
                {
                    TempData["SuccessMessage"] = "Đặt dịch vụ thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi đặt dịch vụ. Vui lòng thử lại sau.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
