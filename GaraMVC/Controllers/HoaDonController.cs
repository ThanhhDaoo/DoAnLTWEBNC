using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;
using GaraMVC.ViewModels;
using GaraMVC.Filters;

namespace GaraMVC.Controllers
{
    [AuthorizeAdmin]
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

        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                var hoaDons = await _hoaDonService.GetAllAsync();

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Hóa đơn");

                    // Header
                    worksheet.Cell(1, 1).Value = "Mã HĐ";
                    worksheet.Cell(1, 2).Value = "Khách hàng";
                    worksheet.Cell(1, 3).Value = "Số điện thoại";
                    worksheet.Cell(1, 4).Value = "Biển số xe";
                    worksheet.Cell(1, 5).Value = "Ngày lập";
                    worksheet.Cell(1, 6).Value = "Tổng tiền";
                    worksheet.Cell(1, 7).Value = "Hình thức TT";
                    worksheet.Cell(1, 8).Value = "Trạng thái";

                    // Style header
                    var headerRange = worksheet.Range(1, 1, 1, 8);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromHtml("#4e73df");
                    headerRange.Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                    headerRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    // Data
                    int row = 2;
                    foreach (var hd in hoaDons)
                    {
                        worksheet.Cell(row, 1).Value = hd.MaHD;
                        worksheet.Cell(row, 2).Value = hd.KhachHang?.TenKH ?? "N/A";
                        worksheet.Cell(row, 3).Value = hd.KhachHang?.SDT ?? "N/A";
                        worksheet.Cell(row, 4).Value = hd.Xe?.BienSo ?? "N/A";
                        worksheet.Cell(row, 5).Value = hd.NgayLap.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(row, 6).Value = hd.TongTien;
                        worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0 ₫";
                        worksheet.Cell(row, 7).Value = hd.HinhThucTT ?? "Chưa TT";
                        worksheet.Cell(row, 8).Value = hd.TrangThai ?? "Mới";
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Add borders
                    var dataRange = worksheet.Range(1, 1, row - 1, 8);
                    dataRange.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;

                    using (var stream = new System.IO.MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        var fileName = $"HoaDon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xuất Excel: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}