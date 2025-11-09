using GaraMVC.Models;

namespace GaraMVC.Services
{
    public interface IYeucauService
    {
        Task<List<YeucauDichVu>> GetAllAsync();
        Task<YeucauDichVu?> GetByIdAsync(int id);
        Task<bool> UpdateStatusAsync(int id, string trangThai);
        Task<bool> DeleteAsync(int id);
    }
}

