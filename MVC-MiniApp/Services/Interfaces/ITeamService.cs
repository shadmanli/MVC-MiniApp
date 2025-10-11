using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.About;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamVM>> GetAllAsync();
        Task CreateAsync(TeamCreateVM request);
        Task<TeamVM>GetByIdAsync(int id);
    }
}
