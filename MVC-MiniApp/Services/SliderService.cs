using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Slider;

namespace MVC_MiniApp.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateSliderAsync(SliderCreateVM request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSliderAsync(Slider slider)
        {
            throw new NotImplementedException();
        }

        public Task EditSliderAsync(Slider slider, SliderEditVM request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SliderInfo>> GetAllAsync()
        {
            return await _context.SliderInfos.ToListAsync();
        }

        public async Task<Slider> GetSliderAsync()
        {
            return await _context.Sliders.FirstOrDefaultAsync();
        }

        public Task<Slider> GetSliderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SliderInfo> GetSliderInfoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
