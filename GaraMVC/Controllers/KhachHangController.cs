using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;

namespace GaraMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            var khachHangs = await _khachHangService.GetAllAsync();
            return View(khachHangs);
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var khachHang = await _khachHangService.GetByIdAsync(id);
            if (khachHang == null)
                return NotFound();

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                var result = await _khachHangService.CreateAsync(khachHang);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm khách hàng!";
                }
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var khachHang = await _khachHangService.GetByIdAsync(id);
            if (khachHang == null)
                return NotFound();

            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhachHang khachHang)
        {
            if (id != khachHang.MaKH)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _khachHangService.UpdateAsync(khachHang);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật khách hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật khách hàng!";
                }
            }
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var khachHang = await _khachHangService.GetByIdAsync(id);
            if (khachHang == null)
                return NotFound();

            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _khachHangService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa khách hàng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa khách hàng!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}