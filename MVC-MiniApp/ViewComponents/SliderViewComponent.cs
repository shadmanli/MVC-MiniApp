using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Slider;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MVC_MiniApp.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderInfoService _sliderInfoService;

        public SliderViewComponent(ISliderService sliderService, ISliderInfoService sliderInfoService)
        {
            _sliderService = sliderService;
            _sliderInfoService = sliderInfoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Tək şəkil
            var slider = await _sliderService.GetSliderAsync();

            // Bütün infos
            var infos = await _sliderInfoService.GetAllUIAsync();

            var model = new SliderUIVM
            {
                Image = slider.Image,
                Infos = infos.ToList() // bütün infos carousel üçün
            };

            return View(model);
        }
    }
}
