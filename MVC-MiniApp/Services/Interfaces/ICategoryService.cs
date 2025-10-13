using Microsoft.AspNetCore.Identity.UI.Services;
using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.CategoryVM;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryUIVM>> GetAllUIAsync();
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<Category> GetByIdAsync(int id); 
        Task CreateAsync(CategoryCreateVM request);
        Task DeleteAsync(Category category); 
        Task EditAsync(Category DbCategory, CategoryEditVM request);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);


    }
}
