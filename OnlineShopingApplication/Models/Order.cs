using System.ComponentModel.DataAnnotations;

namespace OnlineShopingApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Order number")]
        public string OrderNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation property
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
