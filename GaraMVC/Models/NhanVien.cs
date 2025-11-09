using System.ComponentModel.DataAnnotations;
using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class NhanVien
    {
        public int MaNV { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên nhân viên")]
        public string TenNV { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Chức vụ")]
        public string? ChucVu { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string? SDT { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(20)]
        [Display(Name = "Vai trò")]
        public string? VaiTro { get; set; }
    }
}