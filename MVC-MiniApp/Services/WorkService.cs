using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Work;

namespace MVC_MiniApp.Services
{
    public class WorkService : IWorkService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public WorkService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<WorkVM>> GetAllAsync()
        {
            var works = await _context.Works
                .Include(w => w.Category)
                .Include(w => w.Images)
                .ToListAsync();

            return works.Select(w => new WorkVM
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                CategoryName = w.Category.Name,
                MainImage = w.Images.FirstOrDefault(i => i.IsMain)?.Image
            });
        }


        public async Task<Work> GetByIdAsync(int id)
        {
            return await _context.Works
                .Include(w => w.Images)
                .Include(w => w.Category)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task CreateAsync(WorkCreateVM request)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "img");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            List<WorkImage> images = new();

            foreach (var file in request.Images)
            {
                if (!file.ContentType.Contains("image/"))
                    throw new Exception("Fayl şəkil formatında olmalıdır.");

                string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string path = Path.Combine(folderPath, fileName);

                using FileStream stream = new(path, FileMode.Create);
                await file.CopyToAsync(stream);

                images.Add(new WorkImage
                {
                    Image = fileName
                });
            }

            if (images.Count > 0)
                images.First().IsMain = true;

            var work = new Work
            {
                Name= request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Images = images
            };

            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(Work dbWork, WorkEditVM request)
        {
            dbWork.Name = request.Name;
            dbWork.Description = request.Description;
            dbWork.CategoryId = request.CategoryId;

            string folderPath = Path.Combine(_env.WebRootPath, "img");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (request.NewImages != null && request.NewImages.Count > 0)
            {
                foreach (var img in dbWork.Images.ToList())
                {
                    var filePath = Path.Combine(folderPath, img.Image);
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    dbWork.Images.Remove(img);
                }

                bool isFirst = true;
                foreach (var file in request.NewImages)
                {
                    if (!file.ContentType.StartsWith("image/"))
                        throw new Exception("Yüklənən fayl şəkil olmalıdır.");

                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    string path = Path.Combine(folderPath, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    dbWork.Images.Add(new WorkImage
                    {
                        Image = fileName,
                        WorkId = dbWork.Id,
                        IsMain = isFirst
                    });

                    isFirst = false;
                }
            }

            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(Work work)
        {
            foreach (var image in work.Images)
            {
                string filePath = Path.Combine(_env.WebRootPath, "img", image.Image);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkUIVM>> GetAllUIAsync()
        {
            var works = await _context.Works
               .Include(w => w.Category)
               .Include(w => w.Images)
               .ToListAsync();

            return works.Select(w => new WorkUIVM
            {
                Id = w.Id,
                CategoryId = w.CategoryId,
                Description = w.Description,
                CategoryName = w.Category.Name,
                MainImage = w.Images.FirstOrDefault(i => i.IsMain)?.Image
            });
        }

        public async Task<IEnumerable<WorkUIVM>> GetAllRecentWorksUIAsync()
        {
            var works = await _context.Works
              .Include(w => w.Category)
              .Include(w => w.Images)
              .ToListAsync();

            return works.Select(w => new WorkUIVM
            {
                Id = w.Id,
                CategoryId = w.CategoryId,
                Description = w.Description,
                CategoryName = w.Category.Name,
                MainImage = w.Images.FirstOrDefault(i => i.IsMain)?.Image
            }).OrderByDescending(m=>m.Id).Take(6);
        }

        public async Task<WorkVM> GetFirstWorkAsync()
        {
            var work = await _context.Works.FirstOrDefaultAsync();
            return new WorkVM { Name = work.Name, Description = work.Description };
        }
    }
}
