using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Contact
{
    public class ContactCreateVM
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile UploadImage { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
    }
}
