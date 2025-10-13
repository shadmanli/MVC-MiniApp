using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Partner
{
    public class PartnerCreateVM
    {
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
    }
}
