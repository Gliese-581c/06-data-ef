using System;
using System.Collections.Generic;
using System.Linq;
using LuckySpin.Services;

namespace LuckySpin.Models
{
    /**
    * Game Model holds the game logic and in-memory list of Spins as well as the current Player
    * Notice the use of the GameStatus enum to track the current state of the Game
    * this will help control the game play flow in the Controller and Views
    **/
    public class Game
    {
        //TODO: to switch to using the database, comment out this line of code 
        private List<Spin> _spins = new List<Spin>(); //NOTE: This is an in-memory list of spins


        //Game Properties
       //TODO: Add an Id property to the Game class to be used as the primary key in the database
        public Player? Player { get; set; } //The Player playing this Game
        public ICollection<Spin> Spins { get; set; } = new List<Spin>(); //The list of Spins for this Game

        public decimal PlayCost { //Read only - the cost to play a spin
            get { return 0.50m; }
        }
        public GameStatus Status { get; set; } = GameStatus.Idle;
        // Implements the PlayTurn Method as shown in Figure 1. Be sure to set Game Status appropriately
        public void PlayTurn(Spin spin){
            Status = GameStatus.Spinning;
            if (Player?.Balance >= PlayCost)
            {
                Player.Balance -= PlayCost;
                spin.RunningBalance = Player.Balance;

                if (spin.isWinning(Player))
                {
                    Player.Balance += 1.0m;
                    Status = GameStatus.Won;
                }

                //TODO: to switch to using the database, comment out this line of code below
                //Instead, add the spin in the Controller after calling PlayTurn()
                AddSpin(spin);
            }
            else
            {
                Status = GameStatus.GameOver;
            }
        }
        //Game helper methods
        public void Start()
        {
            Reset();
            Status = GameStatus.Spinning;
        }
        public void AddSpin(Spin s)
        {
            Spins.Add(s);
        }
        public void Reset()
        {
            Spins.Clear();
            Status = GameStatus.Idle;
        }
    }

    public enum GameStatus
    {
        Idle,
        Spinning,
        Won,
        GameOver
    }
}