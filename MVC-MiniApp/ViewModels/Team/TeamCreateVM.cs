using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamCreateVM
    {
        [Required(ErrorMessage = "Name is required")]
      
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "You must upload an image")]
        public IFormFile UploadImage { get; set; }
    }
}
