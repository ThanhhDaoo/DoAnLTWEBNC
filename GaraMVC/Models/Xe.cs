using System.ComponentModel.DataAnnotations;
using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class Xe
    {
        public int MaXe { get; set; }

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

        public int MaKH { get; set; }

        public KhachHang? KhachHang { get; set; }
    }
}