using GaraMVC.Models;
using GaraMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaraMVC.Controllers
{
    public class LienHeController : Controller
    {
        private readonly LienHeService _service;
        public LienHeController(LienHeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var ds = await _service.GetAll();
            return View(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, bool daxuly)
        {
            var result = await _service.UpdateStatus(id, daxuly);
            return Json(new { success = result });
        }
    }
}