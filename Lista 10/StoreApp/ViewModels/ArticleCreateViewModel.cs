using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using StoreApp.Util;

namespace StoreApp.ViewModels
{
    public class ArticleCreateViewModel
    {
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "The field {0} must be positive.")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [AllowedExtension(".jpg")]
        public IFormFile? Photo { get; set; }
    }
}
