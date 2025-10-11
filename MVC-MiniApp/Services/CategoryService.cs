using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.CategoryVM;

namespace MVC_MiniApp.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async  Task CreateAsync(CategoryCreateVM request)
        {
               var result= await _context.Categories.AddAsync(new Category
            {
                Name = request.Name,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
           _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category dbCategory, CategoryEditVM request)
        {
            dbCategory.Name = request.Name;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            return await _context.Categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }

        public async Task<IEnumerable<CategoryUIVM>> GetAllUIAsync()
        {
            return await _context.Categories.OrderByDescending(m => m.Id).Select(c => new CategoryUIVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
           return await _context.Categories.FirstOrDefaultAsync(m=>m.Id == id);
        }
    }
}
