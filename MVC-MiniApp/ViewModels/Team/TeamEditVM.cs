using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamEditVM
    {
        public string ExistImage { get; set; }


        public IFormFile UploadImage { get; set; }

        [Required]
        public string Position { get; set; }
        [Required]
        public string Name { get; set; }
        public int  Id { get; set; }
    }
}
