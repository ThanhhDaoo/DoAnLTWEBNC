using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;

namespace GaraMVC.Controllers
{
    public class DichVuController : Controller
    {
        private readonly IDichVuService _dichVuService;

        public DichVuController(IDichVuService dichVuService)
        {
            _dichVuService = dichVuService;
        }

        public async Task<IActionResult> Index()
        {
            var dichVus = await _dichVuService.GetAllAsync();
            return View(dichVus);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dichVu = await _dichVuService.GetByIdAsync(id);
            if (dichVu == null)
                return NotFound();

            return View(dichVu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                var result = await _dichVuService.CreateAsync(dichVu);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dichVu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dichVu = await _dichVuService.GetByIdAsync(id);
            if (dichVu == null)
                return NotFound();

            return View(dichVu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVu dichVu)
        {
            if (id != dichVu.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _dichVuService.UpdateAsync(dichVu);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật dịch vụ thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dichVu);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dichVu = await _dichVuService.GetByIdAsync(id);
            if (dichVu == null)
                return NotFound();

            return View(dichVu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dichVuService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}