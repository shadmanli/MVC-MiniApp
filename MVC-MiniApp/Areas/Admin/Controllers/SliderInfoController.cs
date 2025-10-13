using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Slider;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        
        private readonly ISliderInfoService _sliderInfoService;
        public SliderInfoController(ISliderInfoService  sliderinfoService)
        {
            _sliderInfoService = sliderinfoService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task< IActionResult> Index()
        {
            var result= await _sliderInfoService.GetAllAsync();
            return View(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await _sliderInfoService.GetByIdAsync(id);
            if (slider == null)
                return NotFound();

            return View(slider); 
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAsync(SliderInfoCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);
            await _sliderInfoService.CreateAsync(request);
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _sliderInfoService.DeleteAsync(id);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderInfoService.GetByIdAsync(id);
            if (slider == null)
                return NotFound();

            var res = new SliderInfoEditVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Description = slider.Description
            };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(SliderInfoEditVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _sliderInfoService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }




    }
}
