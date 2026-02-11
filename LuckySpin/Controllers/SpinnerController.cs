using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.Services;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        //DIJ Repository and DbContext objects
        private LuckySpinContext _dbContext; // the database context provides access to the database records for Players and Games
        private Repository _repository; // the repository provides access to the Games Spins
        //Constructor with DIJ Repository object
        public SpinnerController(LuckySpinContext dbContext, Repository repository)
        {
            // (3) saves the DIJ Repository objects into your instance variables
            _dbContext = dbContext;
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

            //Stores the player in the repository
            _dbContext.Players.Add(player);
            var game = new Game(){ 
                Player = player
            };
            _dbContext.Games.Add(game);
            _dbContext.SaveChanges(); //Only after saving changes to the database will the Game have an Id, which is needed to link the Game to the Spins in the next Action

            game.Start();

            return RedirectToAction("Spin", new{gameId = game.Id}); //Redirect to the Spin Action, passing the Game as a parameter
        }

        /***
         * Spin Action (GET only, no data from the View)
         **/       
        public IActionResult Spin(int gameId)
        {
            Game Game = _repository.getGame(gameId); // Get sthe Game from the repository to ensure you have the most up to date version of the Game with all the Spins
            //Plays a turn with a new Spin
            Spin spin = new Spin() { Game = Game, GameId = Game.Id }; //Creates a new Spin, setting the Game and GameId properties to link it to the current Game
            Game.PlayTurn(spin);
            Game.Spins.Add(spin);
            _dbContext.SaveChanges();
            //Checks to see if the game is done (HINT: Use the Game Status)
            //.     if so, redirect to the LuckList Action to show the list of spins
            if (Game.Status == GameStatus.GameOver) 
            {
                return RedirectToAction("LuckList", new {gameId = gameId}); 
            }
            return View("Spin", Game); //TODO:Pass the Game to the Spin View to show the current game results
        }

        /***
         * ListSpins Action (Get only, no data from the View)
         **/
        [HttpGet]
        public IActionResult LuckList(int gameId)
        {
             // Passes the repository to the View to display the game results
            return View(_repository.getGame(gameId));
        }

    }
}

