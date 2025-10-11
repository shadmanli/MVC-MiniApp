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
                return await _context.Teams.Select(t=>new TeamVM
            {
                Name = t.Name,
                Image= t.Image,
                Position= t.Position,

            }).ToListAsync();
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
                Image=fileName

            };
            await  _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();


        }

       

        Task<TeamVM> ITeamService.GetByIdAsync(int id)
        {
            return await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
