using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamVM>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task CreateAsync(TeamCreateVM request);
        Task EditAsync(TeamEditVM request);
        Task DeleteAsync(Team team);

    }
}
