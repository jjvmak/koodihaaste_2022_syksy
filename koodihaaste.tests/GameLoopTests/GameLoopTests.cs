using koodihaaste_2022_syksy;
using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace koodihaaste.tests.GameLoopTests
{
    public class GameLoopTests
    {
        [Fact]
        public void TestGameLoop()
        {
            var game = createNewGame();
            var gameLoop = new GameLoop(game);
            gameLoop.StartGame();

            Assert.Equal(GameState.Ended, game.gameState);
            Assert.Equal(GameResult.Hero1Won, game.gameResult);
        }
        private Game createNewGame()
        {
            var porkkana = new HeroModel()
            {
                Name = "porkkana",
                Stats = new Stats()
                {
                    Health = 33,
                    Attack = 5.6,
                    Defence = 0.6,
                    Delay = 6.4
                }
            };

            var paprika = new HeroModel()
            {
                Name = "paprika",
                Stats = new Stats()
                {
                    Health = 30,
                    Attack = 6.0,
                    Defence = 1.0,
                    Delay = 7.3
                }
            };
            var game = new Game(porkkana, paprika);

            return game;
        }

    }
}
