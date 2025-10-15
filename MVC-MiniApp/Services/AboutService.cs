using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.About;

namespace MVC_MiniApp.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<AboutVM> GetAboutAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about == null) return null;

            return new AboutVM
            {
                Id = about.Id,
                Description = about.Description,
                Image = about.Image
            };
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.FindAsync(id);
        }

        public async Task CreateAsync(AboutCreateVM request)
        {
            string fileName = null;

            if (request.Image != null)
            {
                fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }
            }

            var about = new About
            {
                Description = request.Description,
                Image = fileName
            };

            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(About dbAbout, AboutEditVM request)
        {
            if (request.UploadImage != null)
            {
               
                if (!string.IsNullOrEmpty(dbAbout.Image))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img", dbAbout.Image);
                    if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                }

                string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;
                string newFilePath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                dbAbout.Image = fileName;
            }

            dbAbout.Description = request.Description;
            _context.Abouts.Update(dbAbout);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(About about)
        {
            if (!string.IsNullOrEmpty(about.Image))
            {
                var file = Path.Combine(_env.WebRootPath, "img", about.Image);
                if (File.Exists(file))
                    File.Delete(file);
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
        }

    }
}
