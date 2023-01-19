using System;
using System.Collections.Generic;
using LuckySpin.Models;

namespace LuckySpin.Services
{
    public class Repository
    {
        private List<Spin> spins = new List<Spin>();

       //Property
       public IEnumerable<Spin> PlayerSpins {

            get { return spins; }
       }

        //Access method
        public void AddSpin(Spin s)
        {
            spins.Add(s);
        }
    }
}
