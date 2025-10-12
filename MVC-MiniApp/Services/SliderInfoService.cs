using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Slider;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;
        public SliderInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SliderInfoCreateVM request)
        {
            SliderInfo slider = new SliderInfo
            {
                Title = request.Title,
                Description = request.Description
            };

            await _context.AddAsync(slider);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var slider = await _context.SliderInfos.FindAsync(id);
            if (slider == null) return;

            _context.SliderInfos.Remove(slider);
            await _context.SaveChangesAsync();
        }




        public async Task EditAsync(SliderInfoEditVM request)
        {
            var dbSlider = await _context.SliderInfos.FindAsync(request.Id);
            if (dbSlider == null) return;

            dbSlider.Title = request.Title;
            dbSlider.Description = request.Description;

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<SliderInfoVM>> GetAllAsync()
        {
            return await _context.SliderInfos.Select(m => new SliderInfoVM
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
            }).ToListAsync();
        }

        public  async Task<IEnumerable<SliderInfoUIVM>> GetAllUIAsync()
        {
            return await _context.SliderInfos.Select(m => new SliderInfoUIVM
            {
               
                Title = m.Title,
                Description = m.Description,
            }).ToListAsync();

        }

        public async Task<SliderInfoVM> GetByIdAsync(int id)
        {
            return await _context.SliderInfos.Where(m => m.Id == id).Select(c => new SliderInfoVM
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
            }).FirstOrDefaultAsync();
        }

        

    }


}