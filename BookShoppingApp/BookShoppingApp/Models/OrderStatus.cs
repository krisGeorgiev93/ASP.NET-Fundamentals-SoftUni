using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string StatusName { get; set; } = null!;
    }
}
