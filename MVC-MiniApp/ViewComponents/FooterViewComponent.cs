using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;

namespace MVC_MiniApp.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            var setting = await _settingService.GetAsync();
            return await Task.FromResult(View(setting));
        }
    }
}
