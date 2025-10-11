using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels.Slider;

namespace MVC_MiniApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderInfo>> GetAllAsync();
        Task<Slider> GetSliderAsync(); //return vm olmalidir
        Task CreateSliderAsync(SliderCreateVM request);

        Task EditSliderAsync(Slider slider, SliderEditVM request);


        Task DeleteSliderAsync(Slider slider); //id gelmelidir
        
        Task<Slider> GetSliderByIdAsync(int id);
        Task<SliderInfo> GetSliderInfoByIdAsync(int id);




    }
}
