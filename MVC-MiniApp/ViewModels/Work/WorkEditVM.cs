using System.ComponentModel.DataAnnotations;

namespace MVC_MiniApp.ViewModels.Work
{
    public class WorkEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<WorkImageVM>? ExistingImages { get; set; }

        public List<IFormFile>? NewImages { get; set; }
    }
}
