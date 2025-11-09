using Microsoft.EntityFrameworkCore;
using GaraAPI.Models;

namespace GaraAPI.Data
{
    public class GaraDbContext : DbContext
    {
        public GaraDbContext(DbContextOptions<GaraDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Xe> Xes { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHDDV> ChiTietHDDVs { get; set; }
        public DbSet<ChiTietHDSP> ChiTietHDSPs { get; set; }
        public DbSet<YeucauDichVu> YeucauDichVus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình computed columns
            modelBuilder.Entity<ChiTietHDDV>()
                .Property(c => c.ThanhTien)
                .HasComputedColumnSql("[SoLuong] * [DonGia]");

            modelBuilder.Entity<ChiTietHDSP>()
                .Property(c => c.ThanhTien)
                .HasComputedColumnSql("[SoLuong] * [DonGia]");
        }
    }
}