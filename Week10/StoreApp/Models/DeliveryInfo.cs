using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Models
{
    public class DeliveryInfo
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string FullName { get; set; } = default!;

        [Display(Name = "Phone number")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string PhoneNumber { get; set; } = default!;

        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Country { get; set; } = default!;

        [Display(Name = "Postal code")]
        [DataType(DataType.PostalCode)]
        [MinLength(6, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(6, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string PostalCode { get; set; } = default!;

        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string City { get; set; } = default!;

        [MinLength(3, ErrorMessage = "The field {0} must not be shorter than {1}.")]
        [MaxLength(100, ErrorMessage = "The field {0} must not be longer than {1}.")]
        public string Street { get; set; } = default!;

        [Display(Name = "House number")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be positive.")]
        public int HouseNumber { get; set; }

        [Display(Name = "Apartment number")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be positive.")]
        public int? ApartmentNumber { get; set; }

        [NotMapped]
        [Display(Name = "Delivery address")]
        public string Address
        {
            get
            {
                var number = ApartmentNumber is { } apartmentNumber
                    ? $"{HouseNumber}/{apartmentNumber}"
                    : HouseNumber.ToString();

                return $"{Street} {number}\n{PostalCode} {City}\n{Country}";
            }
        }
    }
}
