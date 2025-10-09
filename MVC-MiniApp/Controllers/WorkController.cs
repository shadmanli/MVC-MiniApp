using Microsoft.AspNetCore.Mvc;

namespace MVC_MiniApp.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
