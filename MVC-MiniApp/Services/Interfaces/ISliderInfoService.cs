using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Partner;
using MVC_MiniApp.ViewModels.Slider;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<IEnumerable<SliderInfoVM>> GetAllAsync();
        Task<IEnumerable<SliderInfoUIVM>> GetAllUIAsync();
        Task<SliderInfoVM> GetByIdAsync(int id);
        Task CreateAsync(SliderInfoCreateVM request);
        Task DeleteAsync(int id);
       Task EditAsync( SliderInfoEditVM request);

















    }
}
