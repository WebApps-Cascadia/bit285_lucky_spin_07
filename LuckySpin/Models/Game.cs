using System;
using System.Collections.Generic;
using System.Linq;
using LuckySpin.Services;

namespace LuckySpin.Models
{
    /**
    * Game Model holds the game logic and in-memory list of Spins as well as the current Player
    * Notice the use of the GameStatus enum to track the current state of the Game
    * this will help control the game play flow in the Controller and Views
    **/
    public class Game
    {
        //Instance Variables
        private List<Spin> _spins = new List<Spin>(); //NOTE: This is an in-memory list of spins

        //Game Properties
        public Player? Player { get; set; } //The Player playing this Game
        public IEnumerable<Spin> Spins { //Read only - the spins in the game
            get { return _spins; }
        }
        public decimal PlayCost { //Read only - the cost to play a spin
            get { return 0.50m; }
        }
        public GameStatus Status { get; set; } = GameStatus.Idle;
            /**
             * GAME PLAY LOGIC TODOs
             *   STEP 1) See if the Player has enough $ to play
             *      - if balance is greater than cost to play, deduct play cost and do step 2 
             *      - else, set Game Status to Lost
             *        
             *   STEP 2) Check if the Player's Spin is a winner
             *       - if so, increase the balance by $1
             *       - go onto step 3
             * 
             *   STEP 3) Update the repository with the new Spin, balance and Player info
             *       - Add a new SpinBalance record for this Spin
             *       - Update the Player object with the current balance 
             *       - go to the Spin View
             */
        //TODO: Implement the PlayTurn Method
        public void PlayTurn(Spin spin){
            Status = GameStatus.Spinning;
            //TODO: Step 1: Charge for Play
            if (Player?.Balance >= PlayCost)
            {
                Player.Balance -= PlayCost;
                spin.BalanceAfterSpin = Player.Balance;
 
                //TODO: Step 2: Check if Winner
                if (spin.isWinning(Player))
                {
                    Player.Balance += 1.0m;
                    Status = GameStatus.Won;
                }

                //TODO: Step 3 : Update the Spins list
                AddSpin(spin);
            }
            else
            {
                Status = GameStatus.GameOver;
            }
        }
        //Game helper methods
        public void Start()
        {
            Reset();
            Status = GameStatus.Spinning;
        }
        public void AddSpin(Spin s)
        {
            _spins.Add(s);
        }
        public void Reset()
        {
            _spins.Clear();
            Status = GameStatus.Idle;
        }
    }

    public enum GameStatus
    {
        Idle,
        Spinning,
        Won,
        GameOver
    }
}