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
        //TODO: Include a DIJ DbContect object here to allow you to save changes to the database
        private LuckySpin.Services.LuckySpinContext _context;
        private Repository _repository; // the repository provides access to the Games Spins
        //Constructor with DIJ Repository object
        public SpinnerController (Repository repository, LuckySpinContext context)
        {
            // (3) saves the DIJ Repository objects into your instance variables

            _repository = repository;
            _context = context;
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

            //TODO: Replace the SingletonUse DbContext to Add and Save the Player to the database
            _context.Players.Add(player);
            _context.SaveChanges();
            _repository.Player = player; 
            
            //TODO:Create a new Game with this Player and store it in the repository
            Game game = new Game(){ 
                Player = player
            };

            //TODO: Replace the SingletonUse DbContext to Add and Save the Player to the database
            _context.Games.Add(game);
            _context.SaveChanges();
            _repository.Game = game;

            //TODO: Start the Game
            _repository.Game.Start();

            return RedirectToAction("Spin", new { Id = game.Id }); //TODO:Redirect to the Spin Action, passing the Game ID as a parameter
        }

        /***
         * Spin Action (GET only, no data from the View)
         **/       
        public IActionResult Spin(int Id)
        {
             //TODO: Edit the line below to get the Game from the database using the Repository methods and the Id parameter
              Game game = _repository.Game;
            
            //TODO:  Set the new Spin's the Game and GameId properties to link it database game
            Spin spin = new Spin();
            spin.Game = game;
            spin.GameId = game.Id;

            //TODO: Replace the Singleton _repository.Game with the game object from the database
            game.PlayTurn(spin);

            // TODO: Use the DbContext to Add and save the spin to the Database
            _context.Spins.Add(spin);
            _context.SaveChanges();

            //Checks to see if the game is done (HINT: Use the Game Status)
            //.     if so, redirect to the LuckList Action to show the list of spins
            if (game.Status == GameStatus.GameOver) 
            {
                return RedirectToAction("LuckList", new {gameId = Id}); //TODO:Redirect to the LuckList Action, passing the Game as a parameter
            }
             //TODO:Pass the Game to the Spin View to show the current game results
            return View("Spin", game);
        }
        /***
         * ListSpins Action (Get only, no data from the View)
         **/
        [HttpGet]
        public IActionResult LuckList(int gameId)
        {
             //TODO: Edit this line to pass the database game to the View using the Repository and the gameId parameter
            return View(_repository.Game);
        }

    }
}

