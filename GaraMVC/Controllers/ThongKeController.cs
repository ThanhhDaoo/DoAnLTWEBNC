using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.ViewModels;

namespace GaraMVC.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly IThongKeService _thongKeService;

        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ThongKeViewModel
            {
                TongQuan = await _thongKeService.GetTongQuanAsync(),
                DoanhThuTheoThang = await _thongKeService.GetDoanhThuTheoThangAsync(),
                TopDichVu = await _thongKeService.GetTopDichVuAsync(5),
                TopSanPham = await _thongKeService.GetTopSanPhamAsync(5),
                HoaDonGanDay = await _thongKeService.GetHoaDonGanDayAsync(10)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoanhThuTheoThang()
        {
            var data = await _thongKeService.GetDoanhThuTheoThangAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopDichVu(int top = 5)
        {
            var data = await _thongKeService.GetTopDichVuAsync(top);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopSanPham(int top = 5)
        {
            var data = await _thongKeService.GetTopSanPhamAsync(top);
            return Json(data);
        }
    }
}