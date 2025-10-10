using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Recent;

namespace MVC_MiniApp.Services
{
    public class RecentService : IRecentService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RecentService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<RecentVM>> GetAllAsync()
        {
            return await _context.Recents
                .Select(r => new RecentVM
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Image = r.Image
                }).ToListAsync();
        }

        public async Task<Recent> GetByIdAsync(int id)
        {
            return await _context.Recents.FindAsync(id);
        }

        public async Task CreateAsync(RecentCreateVM request)
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

            var recent = new Recent
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            };

            _context.Recents.Add(recent);
            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(Recent dbRecent, RecentEditVM request)
        {
            if (request.UploadImage != null)
            {
                // Köhnə faylın silinməsi
                string oldFilePath = Path.Combine(_env.WebRootPath, "img", dbRecent.Image ?? "");
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);

                // Yeni faylın yaradılması
                string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;
                string newFilePath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                dbRecent.Image = fileName;
            }

            // Digər property-lərin güncellenməsi
            dbRecent.Title = request.Title;
            dbRecent.Description = request.Description;

            _context.Recents.Update(dbRecent);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Recent recent)
        {
            // Faylı sil
            if (!string.IsNullOrEmpty(recent.Image))
            {
                var file = Path.Combine(_env.WebRootPath, recent.Image.TrimStart('/'));
                if (File.Exists(file))
                    File.Delete(file);
            }

            _context.Recents.Remove(recent);
            await _context.SaveChangesAsync();
        }
    }
}
