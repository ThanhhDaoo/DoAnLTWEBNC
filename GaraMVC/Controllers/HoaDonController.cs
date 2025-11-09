using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;
using GaraMVC.ViewModels;

namespace GaraMVC.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IHoaDonService _hoaDonService;
        private readonly IKhachHangService _khachHangService;
        private readonly IXeService _xeService;
        private readonly IDichVuService _dichVuService;
        private readonly ISanPhamService _sanPhamService;

        public HoaDonController(
            IHoaDonService hoaDonService,
            IKhachHangService khachHangService,
            IXeService xeService,
            IDichVuService dichVuService,
            ISanPhamService sanPhamService)
        {
            _hoaDonService = hoaDonService;
            _khachHangService = khachHangService;
            _xeService = xeService;
            _dichVuService = dichVuService;
            _sanPhamService = sanPhamService;
        }

        public async Task<IActionResult> Index()
        {
            var hoaDons = await _hoaDonService.GetAllAsync();
            return View(hoaDons);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hoaDon = await _hoaDonService.GetByIdAsync(id);
            if (hoaDon == null)
                return NotFound();

            return View(hoaDon);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new HoaDonCreateViewModel
            {
                KhachHangs = await _khachHangService.GetAllAsync(),
                Xes = await _xeService.GetAllAsync(),
                DichVus = await _dichVuService.GetActiveAsync(),
                SanPhams = await _sanPhamService.GetInStockAsync(),
                UserId = 1 // Tạm thời hardcode, trong thực tế lấy từ session
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HoaDonCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _hoaDonService.CreateAsync(viewModel);
                if (result)
                {
                    TempData["SuccessMessage"] = "Tạo hóa đơn thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo hóa đơn!";
                }
            }

            // Reload data for dropdowns
            viewModel.KhachHangs = await _khachHangService.GetAllAsync();
            viewModel.Xes = await _xeService.GetAllAsync();
            viewModel.DichVus = await _dichVuService.GetActiveAsync();
            viewModel.SanPhams = await _sanPhamService.GetInStockAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _hoaDonService.CompleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Hoàn thành hóa đơn thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi hoàn thành hóa đơn!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hoaDonService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa hóa đơn thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa hóa đơn!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetXesByKhachHang(int maKH)
        {
            var xes = await _xeService.GetByKhachHangAsync(maKH);
            return Json(xes);
        }
    }
}