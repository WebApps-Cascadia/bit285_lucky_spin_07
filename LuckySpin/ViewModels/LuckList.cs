using System;
using System.Collections.Generic;
using LuckySpin.Models;

namespace LuckySpin.ViewModels
{
    public class LuckList
    {
        public IEnumerable<PlayerSpin> PlayerSpins { get; set; }
        public IEnumerable<decimal> Wallet { get; set; }
    }



}
