using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<AboutVM>>GetAllAsync();

        Task<About> GetByIdAsync(int id);
        Task CreateAsync(AboutCreateVM request);
        Task EditAsync(About dbAbout, AboutEditVM request);
        Task DeleteAsync(About about);
    }
}
