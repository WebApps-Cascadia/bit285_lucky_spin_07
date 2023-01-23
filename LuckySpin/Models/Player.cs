using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player //TODO: Annotate the Player properties as described in section 3.
    {
        
        public string FirstName { get; set; }

        [Range(1,9, ErrorMessage = "Choose a number")]
        public int Luck { get; set; }

        [Range(3.0,10.0, ErrorMessage = "bet from $3 to $10")]
        public decimal Balance { get; set; }
    }
}