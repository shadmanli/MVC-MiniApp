using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamCreateVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [RegularExpression("^[A-Za-zƏəÖöÜüĞğİıÇçŞş]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [MaxLength(100, ErrorMessage = "Position cannot exceed 100 characters")]
        public string Position { get; set; }

        [Required(ErrorMessage = "You must upload an image")]
        public IFormFile UploadImage { get; set; }
    }
}
