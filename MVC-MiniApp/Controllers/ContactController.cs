using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
