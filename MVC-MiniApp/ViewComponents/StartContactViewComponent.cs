using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniApp.ViewComponents
{
    public class StartContactViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult(View());
        }
    }
}
