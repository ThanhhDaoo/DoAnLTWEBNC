using GaraMVC.Models;
using GaraMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaraMVC.Controllers
{
    public class YeucauController : Controller
    {
        private readonly IYeucauService _yeucauService;

        public YeucauController(IYeucauService yeucauService)
        {
            _yeucauService = yeucauService;
        }

        public async Task<IActionResult> Index()
        {
            var yeucaus = await _yeucauService.GetAllAsync();
            return View(yeucaus);
        }

        public async Task<IActionResult> Details(int id)
        {
            var yeucau = await _yeucauService.GetByIdAsync(id);
            if (yeucau == null)
            {
                return NotFound();
            }

            return View(yeucau);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string trangThai)
        {
            var result = await _yeucauService.UpdateStatusAsync(id, trangThai);
            if (result)
            {
                TempData["SuccessMessage"] = "Đã cập nhật trạng thái thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _yeucauService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Đã xóa yêu cầu thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

