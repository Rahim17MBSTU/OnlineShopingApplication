using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopingApplication.Models
{
    public class Products
    {
        public int Id { get; set; }


        [Required(ErrorMessage="Product name is Required")]
        [Display(Name="Product Name")]
        public string Name { get; set; }


        [Required(ErrorMessage ="Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }



        [Display(Name="Product Image URL")]
        
        public string? Image { get; set; }


        [Display(Name="Product Color")]
        [StringLength(30)]
        public string? ProductColor { get; set; }


        [Required]
        [Display(Name="Available")]
        public bool IsAvailable  { get; set; }

        // Inventory tracking
        [Display(Name = "Stock Quantity")]
        [Range(0, 10000, ErrorMessage = "Stock must be 0-10000")]
        public int StockQuantity { get; set; } = 0;

        // Date tracking
        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now;


        [Required(ErrorMessage ="Product Type is required")]
        [Display(Name="Product Type")]
        public int ProductTypesId { get; set; }


        [ForeignKey("ProductTypesId")]
        public virtual ProductTypes? ProductTypes { get; set; }


        [Required(ErrorMessage ="Special Tag is required")]
        [Display(Name="Special Tag")]
        public int SpecialTagsId { get; set; }


        [ForeignKey("SpecialTagsId")]
        public virtual SpecialTags? SpecialTags { get; set; }

    }
}
