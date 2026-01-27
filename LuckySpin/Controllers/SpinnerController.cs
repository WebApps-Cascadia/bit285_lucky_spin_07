using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.Services;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        /**
         * DIJ in 4 STEPS -
         * (0) Registers the Repository class as a service in Program.cs 
         * (1) adds an instance variable here of type Repository
         * (2) In the Constructor, calls for a DIJ Repository object to be passed to the constructor
         **/
        private Repository _repository;

        //Constructor with DIJ Repository object
        public SpinnerController(Repository repository)
        {
            // (3) saves the DIJ Repository object into your instance variable
            _repository = repository;
        }
        /***
         * Index Action (GET and POST)
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

            //TODO: Store the player in the repository
            _repository.Player = player;
            //TODO:Create a new Game with this Player and store it in the repository
            _repository.Game = new Game(){ 
                Player = player
            };
            //TODO: Start the Game
            _repository.Game.Start();

            return RedirectToAction("Spin");
        }

        /***
         * Spin Action (GET only, no data from the View)
         **/       
        public IActionResult Spin()
        {
            //Plays a turn with a new Spin
            Spin spin = new Models.Spin();  
            _repository.Game.PlayTurn(spin);
            //TODO: check to see if the game is done (HINT: Use the Game Status)
            //.     if so, redirect to the LuckList Action to show the list of spins
            if (_repository.Game.Status == GameStatus.GameOver) 
            {
                return RedirectToAction("LuckList");
            }
            return View("Spin", spin);
        }

        /***
         * ListSpins Action (Get only, no data from the View)
         **/
        [HttpGet]
        public IActionResult LuckList()
        {
            // Passes the repository to the View to display the game results
            return View(_repository);
        }

    }
}

