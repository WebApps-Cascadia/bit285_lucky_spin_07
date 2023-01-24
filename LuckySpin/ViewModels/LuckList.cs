using System;
using System.Collections.Generic;
using LuckySpin.Models;

namespace LuckySpin.ViewModels
{
    public class LuckList
    {
        public IEnumerable<SpinBalance> SpinBalances { get; set; }
        public Player Player {get;set;} 
    }



}
