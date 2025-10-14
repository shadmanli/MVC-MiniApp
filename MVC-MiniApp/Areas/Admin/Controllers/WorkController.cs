using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkController : Controller
    {
        private readonly IWorkService _workService;
        private readonly ICategoryService _categoryService;

        public WorkController(IWorkService workService, ICategoryService categoryService)
        {
            _workService = workService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var works = await _workService.GetAllAsync();
            return View(works);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            await SetCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(WorkCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                await SetCategoriesAsync();
                return View(request);
            }

            bool exists = await _workService.ExistsByNameAsync(request.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Work with this name already exists");
                await SetCategoriesAsync();
                return View(request);
            }

            await _workService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dbWork = await _workService.GetByIdAsync(id);
            if (dbWork == null) return NotFound();

            var vm = new WorkEditVM
            {
                Id = dbWork.Id,
                Name = dbWork.Name,
                Description = dbWork.Description,
                CategoryId = dbWork.CategoryId,
                Price = dbWork.Price, 
                ExistingImages = dbWork.Images.Select(i => new WorkImageVM
                {
                    Image = i.Image,
                    IsMain = i.IsMain
                }).ToList()
            };

            await SetCategoriesAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(WorkEditVM request)
        {
            var dbWork = await _workService.GetByIdAsync(request.Id);
            if (dbWork == null) return NotFound();

            if (!ModelState.IsValid)
            {
                await SetCategoriesAsync();
                return View(request);
            }

            bool exists = await _workService.ExistsByNameAsync(request.Name, request.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", "Work with this name already exists");
                await SetCategoriesAsync();
                return View(request);
            }

            await _workService.EditAsync(dbWork, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/Work/Delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var work = await _workService.GetByIdAsync(id);
            if (work == null) return NotFound();

            await _workService.DeleteAsync(work);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var dbWork = await _workService.GetByIdAsync(id);
            if (dbWork == null) return NotFound();

            var vm = new WorkDetailVM
            {
                Name = dbWork.Name,
                Description = dbWork.Description,
                CategoryName = dbWork.Category?.Name,
                Price = dbWork.Price,
                Images = dbWork.Images.Select(i => new WorkImageVM
                {
                    Image = i.Image,
                    IsMain = i.IsMain
                }).ToList()
            };

            return View(vm);
        }

        private async Task SetCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }
    }
}
