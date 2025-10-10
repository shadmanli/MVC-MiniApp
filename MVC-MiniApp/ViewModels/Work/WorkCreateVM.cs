using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Work
{
    public class WorkCreateVM
    {
        [Required]
        public string Description { get; set; }
        public string Name { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        public int CategoryId { get; set; }

    }
}
