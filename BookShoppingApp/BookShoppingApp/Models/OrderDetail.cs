using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class OrderDetail
    {

        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public Order Order { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }


    }
}
