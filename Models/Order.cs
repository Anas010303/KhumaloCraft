using System;

namespace WebApplicationPOE1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
