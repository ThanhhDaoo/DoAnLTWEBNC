using System.ComponentModel.DataAnnotations;
using GaraMVC.Models;
namespace GaraMVC.Models
{
    public class HoaDon
    {
        public int MaHD { get; set; }

        [Required]
        public int MaKH { get; set; }

        [Required]
        public int MaXe { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Display(Name = "Ngày lập")]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; } = 0;

        [StringLength(50)]
        [Display(Name = "Hình thức thanh toán")]
        public string? HinhThucTT { get; set; }

        [StringLength(30)]
        [Display(Name = "Trạng thái")]
        public string? TrangThai { get; set; }

        public KhachHang? KhachHang { get; set; }
        public Xe? Xe { get; set; }
        public NhanVien? NhanVien { get; set; }
        public List<ChiTietHDDV>? ChiTietDichVus { get; set; }
        public List<ChiTietHDSP>? ChiTietSanPhams { get; set; }
    }
}