using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels;
using Newtonsoft.Json;

namespace MVC_MiniApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IWorkService _workService;

        public BasketController(IHttpContextAccessor accessor, IWorkService workService)
        {
            _accessor = accessor;
            _workService = workService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basketCookie = _accessor.HttpContext.Request.Cookies["basket"];
            var basketDatas = string.IsNullOrEmpty(basketCookie)
                ? new List<BasketVM>()
                : JsonConvert.DeserializeObject<List<BasketVM>>(basketCookie) ?? new List<BasketVM>();

            List<BasketProductVM> products = new();
            foreach (var item in basketDatas)
            {
                var work = await _workService.GetByIdAsync(item.WorkId);
                if (work == null) continue;

                products.Add(new BasketProductVM
                {
                    WorkId = work.Id,
                    Name = work.Name ?? "No Name",
                    Price = work.Price,
                    Count = item.Count
                });
            }

            ViewBag.TotalPrice = products.Sum(x => x.Price * x.Count);
            return View(products);
        }

        [HttpPost]
        public IActionResult AddToBasket([FromBody] BasketVM data)
        {
            var basketCookie = _accessor.HttpContext.Request.Cookies["basket"];
            var basketDatas = string.IsNullOrEmpty(basketCookie)
                ? new List<BasketVM>()
                : JsonConvert.DeserializeObject<List<BasketVM>>(basketCookie) ?? new List<BasketVM>();

            var existing = basketDatas.FirstOrDefault(x => x.WorkId == data.WorkId);
            if (existing != null)
                existing.Count += data.Count;
            else
                basketDatas.Add(new BasketVM { WorkId = data.WorkId, Count = data.Count });

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return Ok(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveFromBasket([FromBody] BasketVM data)
        {
            var basketCookie = _accessor.HttpContext.Request.Cookies["basket"];
            var basketDatas = string.IsNullOrEmpty(basketCookie)
                ? new List<BasketVM>()
                : JsonConvert.DeserializeObject<List<BasketVM>>(basketCookie) ?? new List<BasketVM>();

            var item = basketDatas.FirstOrDefault(x => x.WorkId == data.WorkId);
            if (item != null)
                basketDatas.Remove(item);

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return Ok(new { success = true });
        }
    }
}
