using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.ViewComponents
{
    public class BannerViewComponent:ViewComponent
    {
        private readonly IAboutService _aboutService;
        public BannerViewComponent(IAboutService aboutservice)
        {
            _aboutService = aboutservice;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
              IEnumerable<AboutVM> about=await _aboutService.GetAllAsync();

            return await Task.FromResult(View(about));
        }
    }
}
