using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Recent;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IRecentService
    {
        Task<List<RecentVM>> GetAllAsync();
        Task<Recent> GetByIdAsync(int id);
        Task CreateAsync(RecentCreateVM request);
        Task EditAsync(Recent dbRecent, RecentEditVM request);
        Task DeleteAsync(Recent recent);
    }
}
