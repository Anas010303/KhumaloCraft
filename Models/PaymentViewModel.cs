using System.ComponentModel.DataAnnotations;

namespace WebApplicationPOE1.Models
{
    public class PaymentViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [CreditCard(ErrorMessage = "Invalid card number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        [DataType(DataType.Date)]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVC is required.")]
        [StringLength(4, ErrorMessage = "Invalid CVC.")]
        public string Cvc { get; set; }
    }
}
