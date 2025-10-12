using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Contact;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var desk = await _contactService.GetContactAsync();
            return View(desk);
        }


        public async Task<IActionResult> Detail(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

     
        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

            await _contactService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) return NotFound();

            var vm = new ContactEditVM
            {
                Id = contact.Id,
                Title = contact.Title,
                Description = contact.Description,
                ExistImage = contact.Image
            };

            return View(vm);
        }

     
        [HttpPost]
        public async Task<IActionResult> Edit(ContactEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            await _contactService.EditAsync(request);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
