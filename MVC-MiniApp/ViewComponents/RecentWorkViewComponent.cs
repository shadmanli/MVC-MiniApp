using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.ViewComponents
{
    public class RecentWorkViewComponent:ViewComponent
    {
        private readonly IWorkService _workService;
        public RecentWorkViewComponent(IWorkService workService)
        {
            _workService = workService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<WorkUIVM> recents = await _workService.GetAllRecentWorksUIAsync();

            return await Task.FromResult(View(recents));
        }
    }
}
