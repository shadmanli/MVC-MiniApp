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
                }).ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task CreateAsync(TeamCreateVM request)
        {
            string fileName = null;

            if (request.UploadImage != null)
            {
                fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;
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

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TeamEditVM request)
        {
            var dbTeam = await _context.Teams.FindAsync(request.Id);
            if (dbTeam == null) return;

            if (request.UploadImage != null)
            {
                
                string oldPath = Path.Combine(_env.WebRootPath, "img", dbTeam.Image ?? "");
                if (File.Exists(oldPath))
                    File.Delete(oldPath);

              
                string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                dbTeam.Image = fileName;
            }

            dbTeam.Name = request.Name;
            dbTeam.Position = request.Position;

            _context.Teams.Update(dbTeam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Team team)
        {
            if (!string.IsNullOrEmpty(team.Image))
            {
                var file = Path.Combine(_env.WebRootPath, "img", team.Image.TrimStart('/'));
                if (File.Exists(file))
                    File.Delete(file);
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

    }
}
