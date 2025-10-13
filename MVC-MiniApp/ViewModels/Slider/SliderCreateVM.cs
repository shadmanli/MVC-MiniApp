using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "You must upload an image")]
        public IFormFile UploadImage { get; set; }
    }
}
