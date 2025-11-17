using Microsoft.AspNetCore.Mvc;
using GaraMVC.Services;
using GaraMVC.Models;
using GaraMVC.ViewModels;

namespace GaraMVC.Controllers
{
    public class XeController : Controller
    {
        private readonly IXeService _xeService;
        private readonly IKhachHangService _khachHangService;

        public XeController(IXeService xeService, IKhachHangService khachHangService)
        {
            _xeService = xeService;
            _khachHangService = khachHangService;
        }

        // GET: Xe
        public async Task<IActionResult> Index()
        {
            var xes = await _xeService.GetAllAsync();
            return View(xes);
        }

        // GET: Xe/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            return View(xe);
        }

        // GET: Xe/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(new XeCreateViewModel());
        }

        // POST: Xe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(XeCreateViewModel model)
        {
            // Validate based on whether adding new customer or selecting existing
            if (model.ThemKhachHangMoi)
            {
                if (string.IsNullOrWhiteSpace(model.TenKH))
                {
                    ModelState.AddModelError("TenKH", "Tên khách hàng là bắt buộc");
                }
                if (string.IsNullOrWhiteSpace(model.SDT))
                {
                    ModelState.AddModelError("SDT", "Số điện thoại là bắt buộc");
                }
            }
            else
            {
                if (!model.MaKH.HasValue || model.MaKH.Value == 0)
                {
                    ModelState.AddModelError("MaKH", "Vui lòng chọn khách hàng");
                }
            }

            if (ModelState.IsValid)
            {
                int maKH;

                // Nếu thêm khách hàng mới
                if (model.ThemKhachHangMoi)
                {
                    var khachHang = new KhachHang
                    {
                        TenKH = model.TenKH!,
                        SDT = model.SDT!,
                        DiaChi = model.DiaChi,
                        Email = model.Email,
                        NgayDangKy = DateTime.Now
                    };

                    var khResult = await _khachHangService.CreateAsync(khachHang);
                    if (!khResult)
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm khách hàng!";
                        ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
                        return View(model);
                    }

                    // Đợi một chút để đảm bảo database đã commit
                    await Task.Delay(500);

                    // Lấy khách hàng vừa tạo để lấy MaKH
                    var newKH = await _khachHangService.GetBySDTAsync(model.SDT!);
                    if (newKH == null)
                    {
                        // Thử lấy lại từ danh sách tất cả khách hàng
                        var allKH = await _khachHangService.GetAllAsync();
                        newKH = allKH.OrderByDescending(k => k.MaKH).FirstOrDefault(k => k.SDT == model.SDT);
                        
                        if (newKH == null)
                        {
                            TempData["ErrorMessage"] = "Không thể lấy thông tin khách hàng vừa tạo!";
                            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
                            return View(model);
                        }
                    }
                    maKH = newKH.MaKH;
                }
                else
                {
                    maKH = model.MaKH!.Value;
                }

                // Tạo xe mới
                var xe = new Xe
                {
                    BienSo = model.BienSo,
                    HangXe = model.HangXe,
                    DoiXe = model.DoiXe,
                    MauXe = model.MauXe,
                    MaKH = maKH
                };

                var result = await _xeService.CreateAsync(xe);
                if (result)
                {
                    TempData["SuccessMessage"] = model.ThemKhachHangMoi 
                        ? "Thêm khách hàng và xe thành công!" 
                        : "Thêm xe thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm xe!";
                }
            }

            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(model);
        }

        // GET: Xe/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(xe);
        }

        // POST: Xe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Xe xe)
        {
            if (id != xe.MaXe)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _xeService.UpdateAsync(xe);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật xe thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật xe!";
                }
            }
            ViewBag.KhachHangs = await _khachHangService.GetAllAsync();
            return View(xe);
        }

        // GET: Xe/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var xe = await _xeService.GetByIdAsync(id);
            if (xe == null)
                return NotFound();

            return View(xe);
        }

        // POST: Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _xeService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa xe thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa xe!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

