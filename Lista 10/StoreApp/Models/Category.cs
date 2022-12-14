using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Name { get; set; } = default!;

        public ICollection<Article>? Articles { get; set; }
    }
}
