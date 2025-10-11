using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public BannerViewComponent(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _aboutService.GetAboutAsync(); 
            return View(about);
        }
    }

}
