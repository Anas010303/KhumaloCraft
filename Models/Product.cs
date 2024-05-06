using System.ComponentModel.DataAnnotations;

namespace WebApplicationPOE1.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name must be between 1 and 100 characters.")]
        public string Name { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "The price must be between 0.01 and 1,000,000.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]  // Consider revising this if your intent is to allow longer text.
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
