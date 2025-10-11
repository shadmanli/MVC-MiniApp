using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController:Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllAsync();
            return View(teams);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _teamService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

      
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var team = await _teamService.GetByIdAsync(id);
        //    if (team == null) return NotFound();

        //    var model = new TeamEditVM
        //    {
        //        Id = team.Id,
        //        Name = team.Name,
        //        Position = team.Position,
        //        UploadImage  = team.Image
        //    };

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _teamService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

      
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            await _teamService.DeleteAsync(team);
            return RedirectToAction(nameof(Index));
        }
    }

}

