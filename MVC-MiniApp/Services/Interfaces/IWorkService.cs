using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.CategoryVM;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkVM>> GetAllAsync();
        Task<Work> GetByIdAsync(int id);
        Task CreateAsync(WorkCreateVM request);
        Task DeleteAsync(Work work);
        Task EditAsync(Work DbWork, WorkEditVM request);

    }
}
