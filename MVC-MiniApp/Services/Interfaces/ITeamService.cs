using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.About;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamVM>> GetAllAsync();
        Task<IEnumerable<TeamUIVM>> GetAllUIAsync();
        Task CreateAsync(TeamCreateVM request);
        Task<TeamVM>GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(TeamEditVM request);
    }
}
