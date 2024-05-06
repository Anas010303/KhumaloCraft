using WebApplicationPOE1.Models;

namespace WebApplicationPOE1.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; } // Assuming UserId is a string
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }

        // Navigation property
        public Product Product { get; set; }
    }
}