using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;

namespace GaraMVC.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;

        public SanPhamController(ISanPhamService sanPhamService)
        {
            _sanPhamService = sanPhamService;
        }

        public async Task<IActionResult> Index()
        {
            var sanPhams = await _sanPhamService.GetAllAsync();
            return View(sanPhams);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sanPham = await _sanPhamService.GetByIdAsync(id);
            if (sanPham == null)
                return NotFound();

            return View(sanPham);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var result = await _sanPhamService.CreateAsync(sanPham);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(sanPham);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sanPham = await _sanPhamService.GetByIdAsync(id);
            if (sanPham == null)
                return NotFound();

            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sanPham)
        {
            if (id != sanPham.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _sanPhamService.UpdateAsync(sanPham);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(sanPham);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _sanPhamService.GetByIdAsync(id);
            if (sanPham == null)
                return NotFound();

            return View(sanPham);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sanPhamService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}