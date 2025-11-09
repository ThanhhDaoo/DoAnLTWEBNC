using GaraMVC.Models;

namespace GaraMVC.Services
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllAsync();
        Task<KhachHang?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KhachHang khachHang);
        Task<bool> UpdateAsync(KhachHang khachHang);
        Task<bool> DeleteAsync(int id);
    }
}