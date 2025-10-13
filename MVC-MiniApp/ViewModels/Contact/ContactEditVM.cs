using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Contact
{
    public class ContactEditVM : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        public string ExistImage { get; set; }

        public IFormFile? UploadImage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(ExistImage) && UploadImage == null)
            {
                yield return new ValidationResult(
                    "You must upload an image",
                    new[] { nameof(UploadImage) }
                );
            }
        }
    }
}
