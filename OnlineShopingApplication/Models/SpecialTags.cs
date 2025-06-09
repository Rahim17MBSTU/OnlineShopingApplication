using System.ComponentModel.DataAnnotations;

namespace OnlineShopingApplication.Models
{
    public class SpecialTags
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Special Tag Type")]
        public string SpecialTagName { get; set; }
    }
}
