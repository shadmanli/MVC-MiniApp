using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MVC_MiniApp.ViewModels.Work
{
    public class WorkCreateVM
    {
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [RegularExpression("^[A-Za-zƏəÖöÜüĞğİıÇçŞş]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "At least one image is required")]
        public List<IFormFile> Images { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
    }
}
