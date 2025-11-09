using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;

namespace GaraMVC.Controllers
{
    public class XeController : Controller
    {
        private readonly IXeService _xeService;
        private readonly IKhachHangService _khachHangService;

        public XeController(IXeService xeService, IKhachHangService khachHangService)
        {
            _xeService = xeService;
            _khachHangService = khachHangService;
        }

        // GET: Xe
        public async Task<IActionResult> Index()
        {
            var xes = await _xeService.GetAllAsync();
            return View(xes);
        }

        // GET: Xe/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            return View(xe);
        }

        // GET: Xe/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View();
        }

        // POST: Xe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Xe xe)
        {
            if (ModelState.IsValid)
            {
                var result = await _xeService.CreateAsync(xe);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm xe thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm xe!";
                }
            }
            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(xe);
        }

        // GET: Xe/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(xe);
        }

        // POST: Xe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Xe xe)
        {
            if (id != xe.MaXe)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _xeService.UpdateAsync(xe);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật xe thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật xe!";
                }
            }
            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(xe);
        }

        // GET: Xe/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            return View(xe);
        }

        // POST: Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _xeService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa xe thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa xe!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

