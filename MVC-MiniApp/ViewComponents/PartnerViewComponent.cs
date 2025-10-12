using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Partner;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.ViewComponents
{
    public class PartnerViewComponent:ViewComponent
    {
        private readonly IPartnerService _partnerService;
        public PartnerViewComponent(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<PartnerUIVM> partner = await _partnerService.GetAllUIAsync();
            return await Task.FromResult(View(partner));
        }
      
    }
}
