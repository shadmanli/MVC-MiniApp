using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.About
{
    public class AboutEditVM 
    {
        public string? ExistImage { get; set; }

        public IFormFile? UploadImage { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description max length is 500 characters")]
        public string Description { get; set; }

        public int Id { get; set; }

    }
}
