using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Slider
{
    public class SliderInfoCreateVM
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        
        public string Description { get; set; }
    }
}
