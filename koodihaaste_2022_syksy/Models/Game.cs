using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.Models
{
    public class Game
    {
        // Contenders
        public HeroModel hero1 { get; set; }
        public HeroModel hero2 { get; set; }

        // Mechanics
        public double gameTime { get; set; } = 0.0;
        public GameState gameState { get; set; } = GameState.Paused;
        public GameResult gameResult { get; set; } = GameResult.UnDecided;
        public GameResult CheckGameStatus()
        {
            if (hero1.StillAlive() && hero2.StillAlive()) return GameResult.UnDecided;
            if (hero1.StillAlive() && !hero2.StillAlive()) return GameResult.Hero1Won;
            if (!hero1.StillAlive() && hero2.StillAlive()) return GameResult.Hero2Won;
            if (!hero1.StillAlive() && !hero2.StillAlive()) return GameResult.Draw;
            else return GameResult.UnDecided;
        }

        public void PerformAttack(HeroModel attacker, HeroModel defender, double gameTime)
        {
            // Calculate damage and set health after attack
            var damageToDefender = attacker.DamageToEnemy(defender.Stats.Defence);
            defender.Stats.Health = defender.HealthAfterAttack(damageToDefender);

            // Set last attack time for the delay penalty
            attacker.lastAttackTime = gameTime;
        }

        public GameResult Tick(double gameTime)
        {
            this.gameTime = gameTime;

            // Game is not started or paused so result should be undecided
            if (this.gameState == GameState.Paused) return GameResult.UnDecided;

            // Perform attacks
            if (hero1.IsAbleToAttack(gameTime)) PerformAttack(hero1, hero2, gameTime);
            if (hero2.IsAbleToAttack(gameTime)) PerformAttack(hero2, hero1, gameTime);
            
            // Set the internal result and return it
            this.gameResult = CheckGameStatus();
            return this.gameResult;
            
        }


        // Creation

        public Game(HeroModel hero1, HeroModel hero2) {
            this.hero1 = hero1;
            this.hero2 = hero2;
        }
    }

    public enum GameState
    {
        Paused,
        Ongoing,
        Ended
    }

    public enum GameResult
    {
        UnDecided, 
        Hero1Won,
        Hero2Won,
        Draw
    }
}
