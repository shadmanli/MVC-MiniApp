using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.CategoryVM
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name max length must be 50")]
        [RegularExpression("^[A-Za-zƏəÖöÜüĞğİıÇçŞş]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }
    }
}
