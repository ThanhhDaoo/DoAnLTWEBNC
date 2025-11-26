using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.ViewModels;
using GaraMVC.Filters;
using System.Diagnostics;

namespace GaraMVC.Controllers
{
    [AuthorizeAdmin]
    public class HomeController : Controller
    {
        private readonly IThongKeService _thongKeService;

        public HomeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model;

            try
            {
                model = await _thongKeService.GetDashboardDataAsync();

                if (model == null)
                    model = new DashboardViewModel();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải dữ liệu: {ex.Message}";
                model = new DashboardViewModel();
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode ?? 500
            };

            if (statusCode.HasValue)
            {
                errorViewModel.ErrorMessage = statusCode.Value switch
                {
                    404 => "Trang bạn tìm kiếm không tồn tại.",
                    403 => "Bạn không có quyền truy cập trang này.",
                    500 => "Lỗi máy chủ. Vui lòng thử lại sau.",
                    _ => "Đã xảy ra lỗi không xác định."
                };
            }

            return View(errorViewModel);
        }
    }
}