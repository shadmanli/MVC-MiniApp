using MVC_MiniApp.ViewModels.About;
using MVC_MiniApp.ViewModels.Contact;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactVM> GetContactAsync();
        Task<ContactUIVM> GetContactUIAsync();
        Task<ContactVM> GetByIdAsync(int id);
        Task CreateAsync(ContactCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(ContactEditVM request);
    }
}
