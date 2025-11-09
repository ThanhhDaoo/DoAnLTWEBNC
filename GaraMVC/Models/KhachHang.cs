using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class KhachHang
    {
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc")]
        [StringLength(100)]
        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime NgayDangKy { get; set; }

        public List<Xe>? DanhSachXe { get; set; }
    }
}
