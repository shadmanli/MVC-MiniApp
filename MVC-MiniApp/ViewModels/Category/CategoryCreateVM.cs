using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Input required")]
        [MaxLength(20, ErrorMessage = "Name max length must be 20")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name can only contain letters")] 
        public string Name { get; set; }
    }
}
