using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Setting;
using NuGet.Configuration;

namespace MVC_MiniApp.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SettingUIVM> GetAsync()
        {
            var result = await _context.Settings.Select(m => new SettingUIVM
            {
                Name = m.Name,
                Description = m.Description,
                
            }).FirstOrDefaultAsync();
            return result;
        }
    }
}
