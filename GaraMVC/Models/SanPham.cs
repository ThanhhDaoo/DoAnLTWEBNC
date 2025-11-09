using System.ComponentModel.DataAnnotations;
using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class SanPham
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; } = string.Empty;

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
        [Display(Name = "Đơn giá")]
        public decimal Gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không được âm")]
        [Display(Name = "Số lượng tồn")]
        public int SoLuongTon { get; set; } = 0;

		[StringLength(255)]
		[Display(Name = "Hình ảnh (URL)")]
		public string? HinhAnh { get; set; }

        [StringLength(20)]
        [Display(Name = "Đơn vị")]
        public string? DonVi { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }
    }
}