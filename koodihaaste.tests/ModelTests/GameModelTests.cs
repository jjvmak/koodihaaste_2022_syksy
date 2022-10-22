using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace koodihaaste.tests.ModelTests
{
    public class GameModelTests
    {
       
        [Fact]
        public void TestGameCreation()
        {
            var game = createNewGame();

            Assert.Equal("porkkana", game.hero1.Name);
            Assert.Equal("paprika", game.hero2.Name);
            Assert.Equal(0.0, game.gameTime);
            Assert.Equal(GameState.Paused, game.gameState);
            Assert.Equal(GameResult.UnDecided, game.gameResult);
        }

        [Fact]
        public void TestGameStatus()
        {
            var game = createNewGame();

            // Both should be alive
            Assert.Equal(GameResult.UnDecided, game.CheckGameStatus());

            // Hero1 should be dead
            game.hero1.Stats.Health = 0;
            Assert.Equal(GameResult.Hero2Won, game.CheckGameStatus());

            // Both are dead
            game.hero2.Stats.Health = 0;
            Assert.Equal(GameResult.Draw, game.CheckGameStatus());
        }

        [Fact]
        public void TestAttacks()
        {
            var game = createNewGame();

            game.PerformAttack(game.hero1, game.hero2, 0.0, false);
            Assert.Equal(24.46, game.hero2.Stats.Health);

            game.PerformAttack(game.hero2, game.hero1, 0.0, false);
            Assert.Equal(27.04, game.hero1.Stats.Health);

        }

        [Fact]
        public void TestTick()
        {
            var gameTime = 0.0;
            var game = createNewGame();

            // Not started yet and timer is not running
            var state = game.Tick(gameTime, false);
            Assert.Equal(GameResult.UnDecided, state);

            // Start the game
            game.gameState = GameState.Ongoing;
            state = game.Tick(gameTime, false);
            Assert.Equal(GameResult.UnDecided, state);
            // Both should have full health
            Assert.Equal(33.0, game.hero1.Stats.Health);
            Assert.Equal(30.0, game.hero2.Stats.Health);

            // Tick 1 second
            gameTime = 1.0;
            state = game.Tick(gameTime, false);
            Assert.Equal(GameResult.UnDecided, state);
            // Neither should have lost any health since delay period has not passed
            Assert.Equal(33.0, game.hero1.Stats.Health);
            Assert.Equal(30.0, game.hero2.Stats.Health);

            // Tick 6.40 seconds
            gameTime = 6.40;
            state = game.Tick(gameTime, false);
            Assert.Equal(GameResult.UnDecided, state);
            // Porkkana has attacked
            Assert.Equal(33.0, game.hero1.Stats.Health);
            Assert.Equal(24.46, game.hero2.Stats.Health);

            // Tick 7.30 seconds
            gameTime = 7.30;
            state = game.Tick(gameTime, false);
            Assert.Equal(GameResult.UnDecided, state);
            // Paprika has attacked
            Assert.Equal(27.04, game.hero1.Stats.Health);
            Assert.Equal(24.46, game.hero2.Stats.Health);

            // Tick untill 38.40 seconds
            while (gameTime < 38.50)
            {
                gameTime += 0.1;
                state = game.Tick(gameTime, false);
            }

            Assert.Equal(GameResult.Hero1Won, state);
          
        }

        private Game createNewGame() {
            var porkkana = new HeroModel()
            {
                Speciality = "not",
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
                Speciality = "not",                
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
