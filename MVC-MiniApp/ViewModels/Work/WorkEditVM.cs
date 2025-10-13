using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MVC_MiniApp.ViewModels.Work
{
    public class WorkEditVM : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [RegularExpression("^[A-Za-zƏəÖöÜüĞğİıÇçŞş]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public List<WorkImageVM>? ExistingImages { get; set; }

        public List<IFormFile>? NewImages { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasExisting = ExistingImages != null && ExistingImages.Count > 0;
            bool hasNew = NewImages != null && NewImages.Count > 0;

            if (!hasExisting && !hasNew)
            {
                yield return new ValidationResult(
                    "At least one image is required",
                    new[] { nameof(NewImages) }
                );
            }
        }
    }
}
