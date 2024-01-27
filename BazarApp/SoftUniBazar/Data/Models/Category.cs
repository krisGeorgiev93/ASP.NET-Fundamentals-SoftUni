using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(15)]
        [Required]
        public string Name { get; set; } = null!;

        public virtual IEnumerable<Ad> Ads { get; set; } = new List<Ad>();
    }
}
