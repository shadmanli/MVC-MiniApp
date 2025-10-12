using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Partner;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerVM>> GetAllAsync();
        Task<IEnumerable<PartnerUIVM>>GetAllUIAsync();
        Task<PartnerVM> GetByIdAsync(int id);
        Task CreateAsync(PartnerCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync( PartnerEditVM request);
    }
}
