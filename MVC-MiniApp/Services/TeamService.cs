using Microsoft.EntityFrameworkCore;

using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Team;

namespace MVC_MiniApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

 
        public async Task<IEnumerable<TeamVM>> GetAllAsync()
        {
            return await _context.Teams
                .Select(t => new TeamVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Position = t.Position,
                    Image = t.Image
                })
                .ToListAsync();
        }

   
        public async Task<TeamVM> GetByIdAsync(int id)
        {
            return await _context.Teams
                .Where(t => t.Id == id)
                .Select(t => new TeamVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Position = t.Position,
                    Image = t.Image
                })
                .FirstOrDefaultAsync();
        }


        public async Task CreateAsync(TeamCreateVM request)
        {
            string fileName = null;

            if (request.UploadImage != null)
            {
                fileName = Guid.NewGuid() + "-" + request.UploadImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }
            }

            var team = new Team
            {
                Name = request.Name,
                Position = request.Position,
                Image = fileName
            };

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

     
        public async Task EditAsync(TeamEditVM request)
        {
            var team = await _context.Teams.FindAsync(request.Id);
            if (team == null) return;

            team.Name = request.Name;
            team.Position = request.Position;

            if (request.UploadImage != null)
            {
              
                if (!string.IsNullOrEmpty(team.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img", team.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

             
                string fileName = Guid.NewGuid() + "-" + request.UploadImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }
                team.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return;

            if (!string.IsNullOrEmpty(team.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "img", team.Image);
                if (File.Exists(path))
                    File.Delete(path);
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

     

        public async Task<IEnumerable<TeamUIVM>> GetAllUIAsync()
        {
            return await _context.Teams
               .Select(t => new TeamUIVM
               {
                   
                   Name = t.Name,
                   Position = t.Position,
                   Image = t.Image
               })
               .ToListAsync();
        }
    }
}
