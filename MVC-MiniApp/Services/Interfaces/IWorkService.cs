using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IWorkService
    {
        Task<IEnumerable<WorkVM>> GetAllAsync();
        Task<IEnumerable<WorkUIVM>> GetAllUIAsync();
        Task<Work> GetByIdAsync(int id); 
        Task CreateAsync(WorkCreateVM request);
        Task DeleteAsync(Work work); 
        Task EditAsync(Work DbWork, WorkEditVM request);
        Task<IEnumerable<WorkUIVM>> GetAllRecentWorksUIAsync();
        Task<WorkVM> GetFirstWorkAsync();
        Task<IEnumerable<OurWorkUIVM>> GetAllUIWorkAsync();
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);



    }
}
