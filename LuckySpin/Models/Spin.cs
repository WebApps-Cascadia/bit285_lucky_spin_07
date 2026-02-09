using System;
using System.Linq;
namespace LuckySpin.Models
{
    public class Spin
    {
        Random random = new Random();
        private int[] numbers; //a spin array;

        //TODO: Add another property to hold the player balance after spin is completed
        public decimal RunningBalance { get; set; }
        //Constructor
        public Spin()
        {
            numbers = new int[] { random.Next(10), random.Next(10), random.Next(10) };
        }

        //Spin Properties
        public int[] Numbers //Read only - the spin numbers are set in the constructor
        { 
            get { return numbers; }
        } 
     
        //Spin Method   
        public bool isWinning(Player player) //true if Player's Luck is one of the numbers
        {
            return (player == null) ?  false : numbers.Contains(player.Luck);
        }
    }

}
