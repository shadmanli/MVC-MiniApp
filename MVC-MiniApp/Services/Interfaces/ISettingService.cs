using MVC_MiniApp.ViewModels.Setting;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ISettingService
    {
        Task<SettingUIVM> GetAsync();
    }
}
