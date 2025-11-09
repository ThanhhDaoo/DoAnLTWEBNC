using GaraMVC.Models;

namespace GaraMVC.Services
{
    public interface ISanPhamService
    {
        Task<List<SanPham>> GetAllAsync();
        Task<List<SanPham>> GetInStockAsync();
        Task<SanPham?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SanPham sanPham);
        Task<bool> UpdateAsync(SanPham sanPham);
        Task<bool> DeleteAsync(int id);
    }
}