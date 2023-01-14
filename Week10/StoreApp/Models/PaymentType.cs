using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public enum PaymentType
    {
        [Display(Name = "Bank transfer")]
        BankTransfer,

        [Display(Name = "Credit card")]
        CreditCard,

        [Display(Name = "Google Pay")]
        GooglePay,

        [Display(Name = "Apple Pay")]
        ApplePay,

        [Display(Name = "BLIK")]
        Blik
    }
}
