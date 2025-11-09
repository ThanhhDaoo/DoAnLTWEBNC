using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaraAPI.Models
{
    [Table("CHITIET_HDDV")]
    public class ChiTietHDDV
    {
        [Key]
        public int MaCTDV { get; set; }

        [Required]
        public int MaHD { get; set; }

        [Required]
        public int MaDV { get; set; }

        public int SoLuong { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal ThanhTien { get; set; }

        [ForeignKey("MaHD")]
        public HoaDon? HoaDon { get; set; }

        [ForeignKey("MaDV")]
        public DichVu? DichVu { get; set; }
    }
}