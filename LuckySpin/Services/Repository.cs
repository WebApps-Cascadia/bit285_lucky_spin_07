using System;
using System.Collections.Generic;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Services
{
    public class Repository
    {
        private List<SpinBalance> playerSpins = new List<SpinBalance>();
     
        //Properties
        public Player Player { get; set; }
        public IEnumerable<SpinBalance> SpinBalances {
            get { return playerSpins; }
        }


        //Access method
        public void AddSpinBalance(Spin spin, decimal balance)
        {
            playerSpins.Add(new SpinBalance
            {
                Spin = spin,
                Balance = balance
            });
        }
        //Game Reset
        public void ResetGame()
        {
            playerSpins.Clear();
        }

    }

}
