using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Recent;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecentController : Controller
    {
        private readonly IRecentService _recentService;

        public RecentController(IRecentService recentService)
        {
            _recentService = recentService;
        }

        public async Task<IActionResult> Index()
        {
            var recents = await _recentService.GetAllAsync();
            return View(recents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecentCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _recentService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recent = await _recentService.GetByIdAsync(id);
            if (recent == null) return NotFound();

            var editModel = new RecentEditVM
            {
                Title = recent.Title,
                Description = recent.Description,
                ExistImage = recent.Image,
                Id = recent.Id,
            };

            return View(editModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecentEditVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var dbRecent = await _recentService.GetByIdAsync(id);
            if (dbRecent == null) return NotFound();

            await _recentService.EditAsync(dbRecent, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var recent = await _recentService.GetByIdAsync(id);
            if (recent == null) return NotFound();

            await _recentService.DeleteAsync(recent);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = _recentService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
    }
}

