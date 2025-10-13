using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email or Username is required")]
        public string EmailOrUsername { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
