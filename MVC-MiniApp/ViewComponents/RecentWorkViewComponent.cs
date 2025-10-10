using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Recent;

namespace MVC_MiniApp.ViewComponents
{
    public class RecentWorkViewComponent:ViewComponent
    {
        private readonly IRecentService _recentService;
        public RecentWorkViewComponent(IRecentService recentservice)
        {
            _recentService = recentservice;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<RecentVM> recent= await _recentService.GetAllAsync();

            return await Task.FromResult(View(recent));
        }
    }
}
