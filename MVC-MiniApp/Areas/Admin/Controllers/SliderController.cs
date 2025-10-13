using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Slider;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _sliderService.GetSliderAsync();
            return View(slider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (ModelState.IsValid)
            {
                await _sliderService.CreateAsync(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _sliderService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null) return NotFound();

            var model = new SliderEditVM
            {
                Id = slider.Id,
            };

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Edit(SliderEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            await _sliderService.EditAsync(request);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
