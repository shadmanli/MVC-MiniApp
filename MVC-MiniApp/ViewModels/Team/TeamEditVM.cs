using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamEditVM : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [RegularExpression("^[A-Za-zƏəÖöÜüĞğİıÇçŞş]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [MaxLength(100, ErrorMessage = "Position cannot exceed 100 characters")]
        public string Position { get; set; }

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
