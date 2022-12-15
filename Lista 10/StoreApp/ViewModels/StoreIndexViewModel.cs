using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreApp.Models;

namespace StoreApp.ViewModels
{
    public class StoreIndexViewModel
    {
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public IEnumerable<Article> Articles { get; set; } = new List<Article>();
    }
}
