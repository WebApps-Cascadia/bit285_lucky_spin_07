using System;
using System.Collections.Generic;
using LuckySpin.Models;

namespace LuckySpin.ViewModels
{
    public class LuckList
    {
        public IEnumerable<Spin> Spins { get; set; }
        public Player Player { get; set; }
    }

}
