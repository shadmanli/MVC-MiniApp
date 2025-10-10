using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;

namespace MVC_MiniApp.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<SliderInfo> SliderInfos = await _sliderService.GetAllAsync();
            Slider Sliders = await _sliderService.GetSliderAsync();

            SliderVCVM result = new()
            {
                SliderInfos = SliderInfos,
                Slider = Sliders
            };

            return await Task.FromResult(View(result));
        }
        public class SliderVCVM
        {
            public Slider Slider { get; set; }
            public IEnumerable<SliderInfo> SliderInfos { get; set; }
        }
    }
}
