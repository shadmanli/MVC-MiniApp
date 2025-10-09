using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniApp.ViewComponents
{
    public class TeamViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult(View());
        }
    }
}
