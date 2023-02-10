using System;
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player
    {

        public long PlayerId { get; set; } //all Entity Models have an Id
        [Required]
        public String FirstName { get; set; }
        public int Luck { get; set; }
        public Decimal Balance { get; set; }

        //TODO: adds the navigation property (relation) between the two Entities
        public ICollection<Spin> Spins { get; set; }
        
    }
}