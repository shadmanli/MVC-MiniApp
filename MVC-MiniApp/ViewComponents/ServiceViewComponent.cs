using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.CategoryVM;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.ViewComponents
{
    public class ServiceViewComponent:ViewComponent
    {
        private readonly IWorkService _workService;
        private readonly ICategoryService _categoryService;
        public ServiceViewComponent(IWorkService workService, ICategoryService categoryService)
        {
            _workService = workService;
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<WorkUIVM> works = await _workService.GetAllUIAsync();
            IEnumerable<CategoryUIVM> categories = await _categoryService.GetAllUIAsync();


            return await Task.FromResult(View(new WorkVCVM
            {
                Works = works,
                Categories = categories
            }));
        }
        public class WorkVCVM
        {
            public IEnumerable<WorkUIVM> Works { get; set; }
            public IEnumerable<CategoryUIVM> Categories { get; set; }
        }
    }
}
