using System.ComponentModel.DataAnnotations;
using GaraMVC.Models;

namespace GaraMVC.ViewModels
{
    public class XeCreateViewModel
    {
        // Thông tin xe
        [Required(ErrorMessage = "Biển số là bắt buộc")]
        [StringLength(20)]
        [Display(Name = "Biển số")]
        public string BienSo { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Hãng xe")]
        public string? HangXe { get; set; }

        [Display(Name = "Đời xe")]
        public int? DoiXe { get; set; }

        [StringLength(30)]
        [Display(Name = "Màu xe")]
        public string? MauXe { get; set; }

        // Chọn khách hàng có sẵn hoặc thêm mới
        [Display(Name = "Khách hàng")]
        public int? MaKH { get; set; }

        [Display(Name = "Thêm khách hàng mới")]
        public bool ThemKhachHangMoi { get; set; }

        // Thông tin khách hàng mới
        [StringLength(100)]
        [Display(Name = "Tên khách hàng")]
        public string? TenKH { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string? SDT { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }
    }
}
