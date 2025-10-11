using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkVM>> GetAllAsync();
        Task<IEnumerable<WorkUIVM>> GetAllUIAsync();
        Task<Work> GetByIdAsync(int id); // return vm olmalidir
        Task CreateAsync(WorkCreateVM request);
        Task DeleteAsync(Work work);  // parametr id gelmelidir
        Task EditAsync(Work DbWork, WorkEditVM request);
        Task<IEnumerable<WorkUIVM>> GetAllRecentWorksUIAsync();
        Task<WorkVM> GetFirstWorkAsync();

    }
}
