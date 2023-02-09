using System;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinDbc _dbc;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinDbc dbc) 
        {
            _dbc = dbc; 

        }

        /***
         * Index Action - Gather Player info
         **/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexVM info)
        {
            if (!ModelState.IsValid) { return View(); }

            //Create a new Player object
            Player player = new Player
            {
                FirstName = info.FirstName,
                Luck = info.Luck,
                Balance = info.StartingBalance
            };
            //Adds the player to dbContext  and save changes to the database
            _dbc.Players.Add(player);
            _dbc.SaveChanges();

            return RedirectToAction("Spin", new { id = player.PlayerId }); 
        }

        /***
         * Spin Action - Play one Spin
         **/  
         [HttpGet]      
         public IActionResult Spin(long id) 
        {
            //TODO: Use Lambda Extention method instead of Find
            Player player = _dbc.Players.Find(id);
            //Player player = _dbc.Players.Include().Single();

            //Intializes the spinItVM with the player object from the database
            SpinVM spinVM = new SpinVM() {
                PlayerName = player.FirstName,
                Luck = player.Luck,
                CurrentBalance = player.Balance
            };

            if (!spinVM.ChargeSpin()) //Charges for a spin or ends the game
            {
                return RedirectToAction("LuckList", new { id = player.PlayerId });
            }
 
            if (spinVM.Winner) { spinVM.CollectWinnings(); } //Pays out if the spin is lucky

            //Sets the player's Balance to their current balance after the spin
            player.Balance = spinVM.CurrentBalance;
            

            //Create a Spin using the logic from the SpinViewModel
            Spin spin = new Spin() {
                IsWinning = spinVM.Winner,
                Balance = spinVM.CurrentBalance
            };

            //TODO: Comment out the line below that Adds spin to the general Spins collection
            _dbc.Spins.Add(spin);
            //TODO: Instead add the spin to this player's collection called "Spins"
            //player.

            _dbc.SaveChanges();

            return View("Spin", spinVM); //Sends the updated spin info to the Spin View
        }

        /***
         * ListSpins Action - Display Spin data
         **/
         [HttpGet]
         public IActionResult LuckList(long id)
        {
            //TODO: Use Include and Single methods instead of Find (don't copy/paste)
            Player player = _dbc.Players.Find(id);


            LuckListVM luckListVM = new LuckListVM
            {
                Player = player,
                //TODO: Send the View only the player's Spins instead of ALL spins
                Spins = _dbc.Spins
             
            };
            return View(luckListVM);
        }

    }
}

