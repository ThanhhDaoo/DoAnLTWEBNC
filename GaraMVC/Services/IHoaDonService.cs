using GaraMVC.Models;
using GaraMVC.ViewModels;

namespace GaraMVC.Services
{
    public interface IHoaDonService
    {
        Task<List<HoaDon>> GetAllAsync();
        Task<HoaDon?> GetByIdAsync(int id);
        Task<bool> CreateAsync(HoaDonCreateViewModel viewModel);
        Task<bool> CompleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}