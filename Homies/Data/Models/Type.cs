using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class Type
    {
        public int Id { get; set; }

        [StringLength(15, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        public virtual IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
