using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopApp.Models
{
	public class User : IdentityUser
	{
        public User()
        {            
            this.Clubs = new HashSet<Club>();
            this.Races = new HashSet<Race>();           
        }


        public int? Pace { get; set; }

        public int? Mileage { get; set; }

        [ForeignKey(nameof(Address))]   
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<Club> Clubs { get; set; }

        public ICollection<Race> Races { get; set; }
    }
}
