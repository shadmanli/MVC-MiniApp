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
        public async Task<IActionResult> Index()
        {
            var result =await _categoryService.GetAllAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _categoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(category);
            return Ok(); 
        }

        [HttpGet]
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
        public async Task<IActionResult> Edit(int id, CategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            await _categoryService.EditAsync(category, request);
            return RedirectToAction(nameof(Index));
        }

    }
}
