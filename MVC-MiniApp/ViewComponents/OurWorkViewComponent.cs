using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;

namespace MVC_MiniApp.ViewComponents
{
    public class OurWorkViewComponent:ViewComponent
    {
        private readonly IWorkService _workService;

        public OurWorkViewComponent(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _workService.GetFirstWorkAsync()));
        }
    }
}
