using MVC_MiniApp.ViewModels.Slider;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<SliderVM> GetSliderAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(SliderEditVM request);
    }
}
