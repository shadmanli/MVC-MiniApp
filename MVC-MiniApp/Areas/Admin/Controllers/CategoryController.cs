using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.CategoryVM;

namespace MVC_MiniApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController:Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var result =await _categoryService.GetAllAsync();
            return View(result);
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            bool exists = await _categoryService.ExistsByNameAsync(request.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Category with this name already exists");
                return View(request);
            }

            await _categoryService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(category);
            return Ok(); 
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)  return NotFound();
            return View(new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(new CategoryEditVM
            {
                Name = result.Name
            });
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id, CategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            bool exists = await _categoryService.ExistsByNameAsync(request.Name, id);
            if (exists)
            {
                ModelState.AddModelError("Name", "Category with this name already exists");
                return View(request);
            }

            await _categoryService.EditAsync(category, request);
            return RedirectToAction(nameof(Index));
        }

    }
}
