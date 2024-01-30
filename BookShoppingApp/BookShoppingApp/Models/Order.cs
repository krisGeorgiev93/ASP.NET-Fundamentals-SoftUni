using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<OrderDetail> OrderDetails { get; set; } = null!;


    }
}
