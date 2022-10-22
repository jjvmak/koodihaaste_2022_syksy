using koodihaaste_2022_syksy.Models;
using koodihaaste_2022_syksy.SpectreOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy
{
    public static class Announcer
    {
        public static void AnnounceAttack(HeroModel attacker, HeroModel defender, double gameTime, double damage)
        {

            SpectreUtils.AnnounceAttack(attacker, defender, gameTime, damage);
        }

        internal static void BattleBegins()
        {
            SpectreUtils.WriteSimpleText("*** OTTELU ALKAA! ***");
        }

        internal static void AnnounceWinner(Game game)
        {
            SpectreUtils.WriteSimpleText("");
            SpectreUtils.WriteSimpleText("");
            SpectreUtils.CreateHeader("VOITTAJA");
            var winner = game.gameResult == GameResult.Hero1Won ? game.hero1 : game.hero2;
            SpectreUtils.HeroDetails(winner);

            var loser = game.gameResult == GameResult.Hero1Won ? game.hero2 : game.hero1;
            SpectreUtils.WriteSimpleText("");
            SpectreUtils.WriteSimpleText("");
            SpectreUtils.CreateHeader("HÄVIÄJÄ");
            SpectreUtils.HeroDetails(loser);


            Console.ReadLine();
        }

        internal static void AnnounceCriticalDamage(HeroModel attacker)
        {
            SpectreUtils.WriteSimpleText($"[darkorange]{attacker.Name.Split(",")[0]} tekee kriittistä vahinkoa![/]");
        }

        internal static void AnnounceMiss(HeroModel attacker)
        {
            SpectreUtils.WriteSimpleText($"[darkorange]{attacker.Name.Split(",")[0]} lyö ohi vastustajan nopeuskenkien vuoksi![/]");
            SpectreUtils.WriteSimpleText("");
        }
    }
}
