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
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "N/A")]
        [Display(Name = "Expiration date")]
        public DateTime? ExpirationDate { get; set; }

        [Required]
        public ArticleCategory Category { get; set; }
    }
}
