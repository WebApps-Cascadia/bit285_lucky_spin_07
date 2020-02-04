using System;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        //TODO: remove reference to the Singleton Repository
        //      and inject a reference (dbcRepo) to the LuckySpinContext 
        Random random = new Random();
        private LuckySpinContext dbcRepo;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinContext dbc)
        {
            dbcRepo = dbc;
        }

        /***
         * Entry Page Action
         **/

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel info)
        {
            if (!ModelState.IsValid) { return View(); }

            //Create a new Player object
            Player player = new Player
            {
                FirstName = info.FirstName,
                Luck = info.Luck,
                Balance = info.StartingBalance
            };
            
            dbcRepo.Players.Add(player);
            dbcRepo.SaveChanges();

            //TODO: Pass the player Id to SpinIt
            return RedirectToAction("SpinIt", new { id = player.Id });
        }

        /***
         * Play through one Spin
         **/  
         [HttpGet]      
         public IActionResult SpinIt(long id) //TODO: receive the player Id
        {
            //TODO: Use the dbcRepo.Player.Find() to get the player object

            // QUESTION 1: Why use the repository player information to initialize the SpinItViewModel?
            //            (HINT: See what happens if you don't initialize it.)
            //TODO: Intialize the spinItVM with the player object from the database
            Player player = dbcRepo.Players.Find(id);

            SpinItViewModel spinItVM = new SpinItViewModel() {
                FirstName = player.FirstName,
                Luck = player.Luck,
                Balance = player.Balance
            };

            // QUESTION 2: What else does ChargeSpin() do besides check if there is enough $$ to spin?
            if (!spinItVM.ChargeSpin())
            {
                return RedirectToAction("LuckList");
            }
            // QUESTION 3: Locate the if-else logic to determine a winning spin?
            //             Why do you think it is done there? 
            if (spinItVM.Winner) { spinItVM.CollectWinnings(); }

            // QUESTION 4: Why is it necessary to update the player's balance from the spinItVM after a spin?
            // TODO: Update the player Balance using the Player from the database
            player.Balance = spinItVM.Balance;

            //Store the Spin in the Repository
            Spin spin = new Spin()
            {
                IsWinning = spinItVM.Winner
            };
            //TODO: Update persistent data using dbcRepo.Spins.Add() and SaveChanges()
            dbcRepo.Spins.Add(spin);
            dbcRepo.SaveChanges();

            return View("SpinIt", spinItVM);
        }

        /***
         * ListSpins Action
         **/
         [HttpGet]
         public IActionResult LuckList()
        {
            //TODO: Pass the View the Spins collection from the dbcRepo
            return View(dbcRepo.Spins);
        }

    }
}

