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
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            return await _context.Sliders
                .Where(s => s.Id == id)
                .Select(s => new SliderVM
                {
                    Id = s.Id,
                    Image = s.Image
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SliderVM> GetSliderAsync()
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync();
            if (slider == null) return null;

            return new SliderVM
            {
                Id = slider.Id,
                Image = slider.Image
            };
        }

    
        public async Task CreateAsync(SliderCreateVM request)
        {
            string fileName = null;
            if (request.UploadImage != null)
            {
                fileName = Guid.NewGuid() + "-" + request.UploadImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }
            }

            var slider = new Slider
            {
                Image = fileName
            };

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(SliderEditVM request)
        {
            var slider = await _context.Sliders.FindAsync(request.Id);
            if (slider == null) return;

            if (request.UploadImage != null)
            {
                if (!string.IsNullOrEmpty(slider.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid() + "-" + request.UploadImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "uploads/sliders", fileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                slider.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return;

            if (!string.IsNullOrEmpty(slider.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.Image);
                if (File.Exists(path))
                    File.Delete(path);
            }

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
    }

}

