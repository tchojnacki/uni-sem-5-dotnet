using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "The field {0} must be positive.")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Guid? PhotoGuid { get; set; }

        public Category? Category { get; set; }

        [NotMapped]
        public string Photo =>
            PhotoGuid is { } guid ? $"/upload/{guid}.jpg" : "/image/placeholder.jpg";
    }
}
