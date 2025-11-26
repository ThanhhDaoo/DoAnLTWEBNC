using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.ViewModels;

namespace GaraMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập rồi thì chuyển về Dashboard
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _logger.LogInformation($"Login attempt for username: {model.Username}");
                
                var user = await _authService.LoginAsync(model.Username, model.Password);

                if (user != null)
                {
                    _logger.LogInformation($"User found: {user.Username}, Role: {user.Role}");
                    
                    if (user.Role == "Admin")
                    {
                        // Lưu thông tin vào Session
                        HttpContext.Session.SetString("UserId", user.UserId.ToString());
                        HttpContext.Session.SetString("Username", user.Username);
                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Role", user.Role);

                        _logger.LogInformation($"User {user.Username} đăng nhập thành công");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        _logger.LogWarning($"User {user.Username} không có quyền Admin. Role: {user.Role}");
                        ModelState.AddModelError("", "Bạn không có quyền truy cập vào trang quản trị.");
                    }
                }
                else
                {
                    _logger.LogWarning($"Login failed for username: {model.Username}");
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng nhập");
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi đăng nhập: {ex.Message}");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
