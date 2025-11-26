using GaraMVC.Models;

namespace GaraMVC.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string username, string password);
    }
}
