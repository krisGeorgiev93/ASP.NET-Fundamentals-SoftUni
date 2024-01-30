using System.ComponentModel.DataAnnotations;

namespace BookShoppingApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BookName { get; set; } = null!;

        public double Price { get; set; }

        public string? Image { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
