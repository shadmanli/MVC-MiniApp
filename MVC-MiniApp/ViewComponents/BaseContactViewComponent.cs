using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;

namespace MVC_MiniApp.ViewComponents
{
    public class BaseContactViewComponent:ViewComponent
    {
        private readonly IContactService _contactService;
        public BaseContactViewComponent(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var exist = await _contactService.GetContactUIAsync(); 
            if (exist == null)
                return Content(""); 
            return View(exist);
        }

    }
}
