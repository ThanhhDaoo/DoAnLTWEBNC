using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GaraWeb.Models;
using GaraWeb.Services;

namespace GaraWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiService _apiService;

    public HomeController(ILogger<HomeController> logger, IApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        // Lấy danh sách dịch vụ và sản phẩm nổi bật để hiển thị trên trang chủ
        var dichVus = await _apiService.GetDichVusAsync();
        var sanPhams = await _apiService.GetSanPhamsAsync();
        
        ViewBag.DichVus = dichVus.Take(3).ToList();
        ViewBag.SanPhams = sanPhams.Take(6).ToList();
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
