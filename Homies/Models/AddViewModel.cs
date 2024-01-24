using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class AddViewModel
    {

        [Required]
        [StringLength(20, MinimumLength = 5,
            ErrorMessage = "Event name must be between 5 and 20 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150, MinimumLength = 15,
            ErrorMessage = "Description must be between 15 and 150 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; }
            = new List<TypeViewModel>();
    }
}
