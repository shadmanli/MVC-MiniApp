using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.About
{
    public class AboutEditVM : IValidatableObject
    {
        public string? ExistImage { get; set; }

        public IFormFile? UploadImage { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description max length is 500 characters")]
        public string Description { get; set; }

        public int Id { get; set; }

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
