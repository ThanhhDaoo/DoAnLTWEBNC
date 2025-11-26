using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.ViewModels;
using GaraMVC.Filters;

namespace GaraMVC.Controllers
{
    [AuthorizeAdmin]
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

        // API cho biểu đồ doanh thu theo ngày (có filter)
        [HttpGet]
        public async Task<IActionResult> GetDoanhThu(DateTime? fromDate, DateTime? toDate)
        {
            var from = fromDate ?? DateTime.Now.AddMonths(-1);
            var to = toDate ?? DateTime.Now;

            var data = await _thongKeService.GetDoanhThuTheoNgayAsync(from, to);
            
            return Json(new
            {
                tongDoanhThu = data.TongDoanhThu,
                tongHoaDon = data.TongHoaDon,
                chiTiet = data.ChiTiet.Select(x => new
                {
                    ngay = x.Ngay,
                    tongTien = x.TongTien
                })
            });
        }

        // API cho biểu đồ top khách hàng
        [HttpGet]
        public async Task<IActionResult> GetTopKhachHang(int top = 10)
        {
            var data = await _thongKeService.GetTopKhachHangAsync(top);
            return Json(data.Select(x => new
            {
                tenKH = x.TenKH,
                tongChiTieu = x.TongChiTieu
            }));
        }

        // API cho biểu đồ dịch vụ phổ biến
        [HttpGet]
        public async Task<IActionResult> GetDichVuPhoBien(int top = 5)
        {
            var data = await _thongKeService.GetTopDichVuAsync(top);
            return Json(data.Select(x => new
            {
                tenDV = x.TenDV,
                soLanSuDung = x.SoLuong
            }));
        }

        // API cho biểu đồ sản phẩm bán chạy
        [HttpGet]
        public async Task<IActionResult> GetSanPhamBanChay(int top = 5)
        {
            var data = await _thongKeService.GetTopSanPhamAsync(top);
            return Json(data.Select(x => new
            {
                tenSP = x.TenSP,
                soLuongBan = x.SoLuong
            }));
        }

        // Giữ lại các API cũ để tương thích
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
