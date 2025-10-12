using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.ViewComponents
{
    public class TeamViewComponent:ViewComponent
    {
        private readonly ITeamService _teamService;
        public TeamViewComponent(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<TeamUIVM> experts = await _teamService.GetAllUIAsync();
            return await Task.FromResult(View(experts));
        }
    }
}
