using System;
using System.Collections.Generic;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Services
{
    public class Repository
    {
        private List<PlayerSpin> spins = new List<PlayerSpin>();
     
        //Properties
        public Player Player { get; set; }

        public IEnumerable<PlayerSpin> PlayerSpins {
            get { return spins; }
        }

        public decimal CurrentBalance
        {
            get {
                PlayerSpin? playerSpin = spins.LastOrDefault<PlayerSpin>();
                return playerSpin?.balance ?? Player.Balance;
               
            }
        }

        //Access method
        public void AddSpinBalance(Spin s, decimal b)
        {
            spins.Add(new PlayerSpin
            {
                Spin = s,
                balance = b
            });
        }
        public void StartGame()
        {
            spins.Clear();
        }

    }

}
