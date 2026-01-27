using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player //TODO: Add and Annotate a decimal property called Balance
    {

        [Required(ErrorMessage ="Name is required")]
        public required string FirstName { get; set; }

        [Range(1,10, ErrorMessage = "Choose a number, 1-10")]
        public int Luck { get; set; }

 
        [Range(3.0, 10.0, ErrorMessage = "bet from $3 to $10")]
        public decimal Balance { get; set; }

    }
}