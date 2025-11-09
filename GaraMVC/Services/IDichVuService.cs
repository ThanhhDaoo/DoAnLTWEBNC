using GaraMVC.Models;

namespace GaraMVC.Services
{
    public interface IDichVuService
    {
        Task<List<DichVu>> GetAllAsync();
        Task<List<DichVu>> GetActiveAsync();
        Task<DichVu?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DichVu dichVu);
        Task<bool> UpdateAsync(DichVu dichVu);
        Task<bool> DeleteAsync(int id);
    }
}