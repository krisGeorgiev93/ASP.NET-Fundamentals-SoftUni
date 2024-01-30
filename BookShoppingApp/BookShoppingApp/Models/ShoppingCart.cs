using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; }
              

        [Required]
        public string UserId { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;





    }
}
