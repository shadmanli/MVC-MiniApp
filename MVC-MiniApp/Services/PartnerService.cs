using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.Partner;

namespace MVC_MiniApp.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly AppDbContext _context;
        public PartnerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(PartnerCreateVM request)
        {
            Partner partner = new Partner
            {
                Image = request.Image,
            };
            await _context.AddAsync(partner);
            await _context.SaveChangesAsync();
        }

        public  async Task DeleteAsync(int id)
        {
            var result = await _context.Partners.FindAsync(id);
            if (result == null) return;
            _context.Partners.Remove(result);
            await _context.SaveChangesAsync();
        }

 
        public async Task EditAsync(PartnerEditVM request)
        {
            var dbPartner = await _context.Partners.FindAsync(request.Id);

            if (dbPartner == null)
                return;
            dbPartner.Image = request.Image;
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<PartnerVM>> GetAllAsync()
        {
            return await _context.Partners.Select(c => new PartnerVM
            {
                Id = c.Id,
                Image = c.Image,
            }).ToListAsync();

        }

        public  async Task<IEnumerable<PartnerUIVM>> GetAllUIAsync()
        {
            return await _context.Partners.Select(c => new PartnerUIVM
            {
               
                Image = c.Image,
            }).ToListAsync();
        }

        public async Task<PartnerVM> GetByIdAsync(int id)
        {
            return await _context.Partners.Where(m => m.Id == id).Select(c => new PartnerVM
            {
                Id = c.Id,
                Image = c.Image
            })
            .FirstOrDefaultAsync();

        }


       
    }
}
