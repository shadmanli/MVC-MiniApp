using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Partner;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;
        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task< IActionResult> Index()
        {
            var result= await _partnerService.GetAllAsync();
            return View(result);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var result= await _partnerService.GetByIdAsync(id);
            return View(result);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAsync(PartnerCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);
            await _partnerService.CreateAsync(request);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _partnerService.DeleteAsync(id);  
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnerService.GetByIdAsync(id);
            if (partner == null)
                return NotFound();

            
            var res = new PartnerEditVM
            {
                Id = partner.Id,
                Image = partner.Image
            };

            return View(res);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(PartnerEditVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _partnerService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }


    }
}
