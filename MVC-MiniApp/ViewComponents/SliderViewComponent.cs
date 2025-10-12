using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;

namespace MVC_MiniApp.ViewComponents
{
    using global::MVC_MiniApp.Services;
    using global::MVC_MiniApp.ViewModels.Slider;
    using global::MVC_MiniApp.ViewModels.Team;
    using Microsoft.AspNetCore.Mvc;
   

    namespace MVC_MiniApp.ViewComponents
    {
        public class SliderViewComponent : ViewComponent
        {
            private readonly  ISliderInfoService _sliderInfoService;
            public SliderViewComponent(ISliderInfoService sliderInfoService)
            {
                _sliderInfoService = sliderInfoService;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {

                IEnumerable<SliderInfoUIVM> infos = await _sliderInfoService.GetAllUIAsync();
                return await Task.FromResult(View(infos));
            }
        }
    }

}
