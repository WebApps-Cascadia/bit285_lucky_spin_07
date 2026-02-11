using System;
using System.Linq;
namespace LuckySpin.Models
{
    public class Spin
    {
        Random random = new Random();
        private int[] numbers; //a spin array;

        public int Id { get; set; }
        //Add a GameId property to link this Spin to the Game who made it (Foreign Key)
        public int GameId { get; set; } //Foreign Key to the Game who made this Spin
        public Game Game { get; set; } //Navigation property to the Game that contains this Spin
        public decimal RunningBalance { get; set; }
        //Constructor
        public Spin()
        {
            numbers = new int[] { random.Next(10), random.Next(10), random.Next(10) };
        }

        //Spin Properties
        public int[] Numbers //the spin numbers are set in the constructor
        { 
            //TODO: Uncomment the line below to Change the Model
            //set { numbers = value; } // adding the set accessor makes the DbContext take note of this as a column.
            get { return numbers; }
        } 
     
        //Spin Method   
        public bool isWinning(Player player) //true if Player's Luck is one of the numbers
        {
            return (player == null) ?  false : numbers.Contains(player.Luck);
        }
    }

}
