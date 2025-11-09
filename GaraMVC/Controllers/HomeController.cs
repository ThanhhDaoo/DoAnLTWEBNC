using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GaraMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IThongKeService _thongKeService;

        public HomeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        public async Task<IActionResult> Index()
        {
            TongQuanViewModel model;

            try
            {
                model = await _thongKeService.GetTongQuanAsync();

                // Nếu service trả về null, khởi tạo object mặc định
                if (model == null)
                    model = new TongQuanViewModel();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, vẫn trả về object rỗng, tránh null reference
                TempData["ErrorMessage"] = $"Không thể tải dữ liệu: {ex.Message}";
                model = new TongQuanViewModel();
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
