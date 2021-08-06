using System.ComponentModel.DataAnnotations;

namespace ShoppingOnline.Models
{
    public class Items
    {
        public int Id { get; set; }

        [Display(Name = "ItemName")]
        public string ItemName { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }
        
        [Display(Name = "Size")]
        public string Size { get; set; }
        
        [Display(Name = "Price")]
        public string Price { get; set; }
    }
}
