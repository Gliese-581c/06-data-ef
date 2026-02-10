using LuckySpin.Models;

namespace LuckySpin.Services
{
    public class Repository
    {   
        // This repository is connection between the Controller and the database relationships
        // using the LuckySpinContext via DIJ. This will allow you to use 
        // methods to query the database for individual Games, Spins and Players based on the GameId, which is the common link between all three entities.
        //TODO: Inject the LuckySpinContext into the Repository using DIJ and save it in an instance variable


        //Methods for the Singleton data store

        public required Player Player { get; set; }
        public required Game Game { get; set; }

        //TODO: After switching to the database, uncomment these lines
        // Methods to get Database Game, Spins and Player for a given GameId
       /**

        public Game getGame(int GameId) {
            return _dbContext.Games.FirstOrDefault(g => g.Id == GameId) ?? new Game();
        }

        public ICollection<Spin> getSpins(int GameId) {
            return _dbContext.Games.FirstOrDefault(g => g.Id == GameId)?.Spins ?? new List<Spin>();
        }   

        public Player getPlayer(int GameId) {
            return _dbContext.Games.FirstOrDefault(g => g.Id == GameId)?.Player ?? new Player();
        }

        **/

    }

}
