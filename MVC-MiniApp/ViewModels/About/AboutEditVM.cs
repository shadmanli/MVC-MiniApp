using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.About
{

    public class AboutEditVM
    {
        public string? ExistImage { get; set; }

        public IFormFile? UploadImage { get; set; }

        [Required]


      
        public string Description { get; set; }

        public int Id { get; set; }
    }


}
