using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IAboutService
    {
        Task<AboutVM> GetAboutAsync(); 
        Task<About> GetByIdAsync(int id); // return type vm olmalidir
        Task CreateAsync(AboutCreateVM request);
        Task EditAsync(About dbAbout, AboutEditVM request);
        Task DeleteAsync(About about); // int id gelmelidir parametr
    }

}
