using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Data;
using MVC_MiniApp.Models;
using MVC_MiniApp.Services.Interfaces;
using MVC_MiniApp.ViewModels.About;
using MVC_MiniApp.ViewModels.Contact;

namespace MVC_MiniApp.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

    
        
        public async Task<ContactVM> GetByIdAsync(int id)
        {
            return await _context.Contacts
                .Where(c => c.Id == id)
                .Select(c => new ContactVM
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Image = c.Image
                })
                .FirstOrDefaultAsync();
        }

        
        public async Task CreateAsync(ContactCreateVM request)
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

            var contact = new Contact
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

    
        public async Task EditAsync(ContactEditVM request)
        {
            var contact = await _context.Contacts.FindAsync(request.Id);
            if (contact == null) return;

            contact.Title = request.Title;
            contact.Description = request.Description;

            if (request.UploadImage != null)
            {
                
                if (!string.IsNullOrEmpty(contact.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img", contact.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                
                string fileName = Guid.NewGuid() + "-" + request.UploadImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                contact.Image = fileName;
            }

            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return;

            if (!string.IsNullOrEmpty(contact.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "img", contact.Image);
                if (File.Exists(path))
                    File.Delete(path);
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async  Task<ContactVM> GetContactAsync()
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync();
            if (contact == null) return null;

            return new ContactVM
            {
                Id = contact.Id,
                Description = contact.Description,
               
                Image = contact.Image,
                Title=contact.Title,
            };
        }

        public async Task<ContactUIVM> GetContactUIAsync()
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync();
            if (contact == null) return null;

            return new ContactUIVM
            {
                Title = contact.Title,
                Description = contact.Description,
                Image = contact.Image
            };

        }
    }
}
