using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.Services;
using System.Globalization;
using static System.Formats.Asn1.AsnWriter;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        /**
         * DIJ in 4 STEPS -
         * 0) Register the Repository class as a service in Program.cs 
         * 1) add an instance variable here of type Repository
         * 2) In the Constructor, call for a DIJ Repository object to be passed to the constructor
         **/
        private Repository repository;
        private decimal balance;

        public SpinnerController(Repository repository)
        {
            // 3) save the DIJ Repository object into your instance variable
            this.repository = repository;
        }

        /***
         * Index Action
         **/
        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if(!ModelState.IsValid) { return View(); }

            //Store the player in the repository
            repository.Player = player;

            //Start a new Game
            repository.StartGame();

            return RedirectToAction("Spin", player);
        }

        /***
         * Spin Action
         **/  
               
        public IActionResult Spin(Player player)
        {
            //Create a new Spin with the player
            Spin spin = new Spin { Player = player };

            /**
             * GAME PLAY LOGIC TODOs
             *   1) See if the Player has enough $ to play
             *      - if balance is less than $0.50, go to LuckList View
             *      - else, charge to play and go on to step 2
             *        
             *   2) Check if the Player's Spin is a winner
             *       - if so, increase the balance by $1
             *       - go onto step 3
             * 
             *   3) Display the Spin View
             *       - Add the new Spin to the repository
             *       - Add the new balance to the repository
             *       - go to the Spin View
             */

            
            balance = repository.CurrentBalance;
            
            //TODO: Step 1
            if (balance < 0.50m)
            {
                return RedirectToAction("LuckList",
                    new LuckList {
                        PlayerSpins = repository.PlayerSpins
                    });
            }
            else { balance -= 0.50m; }

            //TODO: Step 2
            if (spin.IsWinning) {
                balance += 1.00m;
            }

            //TODO: Step 3
            repository.AddSpinBalance(spin,balance);
            return View("Spin", spin);
        }

        /***
         * ListSpins Action
         **/
        [HttpGet]
        public IActionResult LuckList()
        {
            //Create a new LuckList ViewModel
            LuckList luckList = new LuckList
            {
                PlayerSpins = repository.PlayerSpins
            };
            //TODO: Pass the repository's Player Spins to the LuckList View
            return View(luckList);
        }

    }
}

