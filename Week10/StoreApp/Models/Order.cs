using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StoreApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ValidateNever]
        [Display(Name = "Email")]
        public string OwnerName { get; set; } = default!;

        public DeliveryInfo DeliveryInfo { get; set; } = default!;

        [Display(Name = "Payment type")]
        public PaymentType PaymentType { get; set; }

        public ICollection<OrderArticle> Articles { get; set; } = new List<OrderArticle>();

        [NotMapped]
        [Display(Name = "Sum")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice => Articles.Sum(oa => oa.Price);
    }
}
