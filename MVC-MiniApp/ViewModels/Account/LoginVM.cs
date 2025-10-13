using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "EmailorUsername is required")]
        public string EmailOrUsername { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
