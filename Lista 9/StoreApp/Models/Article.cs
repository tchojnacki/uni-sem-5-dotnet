using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public enum ArticleCategory
    {
        Food,
        Drink,
        Clothing,
        Household,
        Office,
        Pet,
        Electronics
    }

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "The field {0} must be positive.")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "N/A")]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Required]
        public ArticleCategory Category { get; set; }
    }
}
