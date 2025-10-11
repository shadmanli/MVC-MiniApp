using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Team;
using System.Collections;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _teamService.GetAllAsync();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await _teamService.CreateAsync(request);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var team= await _teamService.GetByIdAsync(id);
            if(team == null) return NotFound();
            return View(new TeamVM
            {
                Name= team.Name,
                Image= team.Image,
                Position=team.Position,

            });
        }


    }

}

