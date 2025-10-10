using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }

    
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _aboutService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

   
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();

            var vm = new AboutEditVM
            {
                Id = about.Id,
                Description = about.Description,
                ExistImage=about.Image,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();

            await _aboutService.EditAsync(about, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();

            await _aboutService.DeleteAsync(about);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();

            return View(new AboutVM
            {
                Id = about.Id,
                Description = about.Description,
                Image = about.Image
            });
        }
    }
}
