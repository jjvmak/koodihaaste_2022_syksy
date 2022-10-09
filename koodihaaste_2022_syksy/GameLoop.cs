using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy
{
    public class GameLoop
    {
        private Game game { get; set; }
        
        public GameLoop(Game game)
        {
            this.game = game;
        }

        public void StartGame()
        {
            var gameTime = 0.00;
            // Start the game
            game.gameState = GameState.Ongoing;
            // Start the game loop
            var result = GameResult.UnDecided;
            while (result == GameResult.UnDecided)
            {
                // Tick the game
                result = game.Tick(gameTime);
                // Increase game time
                gameTime += 0.01;
            }
            // Game is over
            game.gameState = GameState.Ended;

        }

    }
}
