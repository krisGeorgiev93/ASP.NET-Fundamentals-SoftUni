using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models
{
    public class AdViewShortModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string CreatedOn { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;                

        public string Category { get; set; } = null!;

        public string Owner { get; set; } = null!;
    }
}
