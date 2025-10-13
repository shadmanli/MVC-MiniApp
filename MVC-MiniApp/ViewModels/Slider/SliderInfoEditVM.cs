using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Slider
{
    public class SliderInfoEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
  
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]

        public string Description { get; set; }
    }
}
