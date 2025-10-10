using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile UploadImage { get; set; }
    }
}
