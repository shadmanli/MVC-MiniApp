using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Recent
{
    public class RecentEditVM
    {
        [Required]
        public string ExistImage { get; set; }
    

        public IFormFile UploadImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
