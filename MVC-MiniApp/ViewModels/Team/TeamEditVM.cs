using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string ExistImage { get; set; }
        public IFormFile? UploadImage { get; set; } //
    }
}
