using LuckySpin.Models;

namespace LuckySpin.Services
{
    public class Repository
    {   
        /**
         * NOTE: The repository will eventually be the interface to a database
         *       and will store and retrieve Player and Game data from the database records.
         *      For now, it just holds the references to the current Player and Game in memory
         **/

        public required Player Player { get; set; }
        //TODO: Add the Game Model
        public required Game Game { get; set; }
        


    }

}
