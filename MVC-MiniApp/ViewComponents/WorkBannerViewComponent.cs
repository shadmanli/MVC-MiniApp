using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Partner;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.ViewComponents
{
    public class WorkBannerViewComponent:ViewComponent
    {
        private readonly IWorkService _workService;
        public WorkBannerViewComponent(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<OurWorkUIVM> works = await _workService.GetAllUIWorkAsync();
            return await Task.FromResult(View(works));
        }
    }
}
