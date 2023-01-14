using System.ComponentModel.DataAnnotations;

namespace StoreApp.DTOs
{
    public class CreateCategoryDto
    {
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = default!;
    }
}
