using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniApp.ViewComponents
{
    public class BannerViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult(View());
        }
    }
}
