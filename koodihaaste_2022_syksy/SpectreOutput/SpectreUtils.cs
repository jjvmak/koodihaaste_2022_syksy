using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using koodihaaste_2022_syksy.Models;
using Spectre.Console;

namespace koodihaaste_2022_syksy.SpectreOutput
{
    public static class SpectreUtils
    {
        public static void CreateHeader(string header)
        {
            AnsiConsole.Write(
                new FigletText(header)
                    .Centered()
                    .Color(Color.Magenta2_1));
        }

        public static void WriteSimpleText(string text)
        {
            AnsiConsole.MarkupLine(text);
        }

        public async static Task Proggres(string message)
        {
            // Asynchronous
            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    // Define tasks
                    var task1 = ctx.AddTask($"[green]{message}[/]");
                    // var task2 = ctx.AddTask("[green]Folding space[/]");

                    while (!ctx.IsFinished)
                    {
                        // Simulate some work
                        await Task.Delay(100);

                        // Increment
                        task1.Increment(10);
                        
                    }
                });
        }

        internal static void HeroDetails(HeroModel hero)
        {
            var table = new Table();
            table.Centered();
            table.AddColumn("Nimi");
            table.AddColumn(new TableColumn("Energiaa jäljellä"));
            table.AddColumn(new TableColumn("Hyökkäys"));
            table.AddColumn(new TableColumn("Puolustus"));
            table.AddColumn(new TableColumn("Viive"));
            table.AddColumn(new TableColumn("Spesiaali esine"));
            table.AddColumn(new TableColumn("Hyökkäyskerrat"));
            table.AddRow(
                   Truncate($"{hero.Name}", 30),
                   $"[red]{Math.Round(hero.Stats.Health, 2)}[/]",
                   $"[orange3]{Math.Round(hero.Stats.Attack, 2)}[/]",
                   $"[blue]{Math.Round(hero.Stats.Defence, 2)}[/]",
                   $"[purple]{Math.Round(hero.Stats.Delay, 2)}[/]",
                   $"[green]{hero.Speciality}[/]",
                   $"[yellow]{hero.AttackCount}[/]"
                );
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }

        public static void AnnounceAttack(HeroModel attacker, HeroModel defender, double gameTime, double damage)
        {
            var time = Math.Round(gameTime, 2);
            var attackerName = attacker.Name.Split(",")[0];
            var defenderName = defender.Name.Split(",")[0];

            var attackerWithColor = $"[{attacker.Color}]{attackerName}[/]";
            var defenderWithColoer = $"[{defender.Color}]{defenderName}[/]";

            var damageWithColor = $"[green]{damage}[/]";

            var timeWithColor = $"[magenta]{time}[/]";

            SpectreUtils.WriteSimpleText($"{timeWithColor}: {attackerWithColor} tekee {damageWithColor} vahinkoa");
            SpectreUtils.WriteSimpleText($"--- {defenderWithColoer} [green]{defender.Stats.Health}[/] energiaa jäljellä ---");
            SpectreUtils.WriteSimpleText("");
        }

        internal static void SelectedHeroes(HeroModel? hero1, HeroModel? hero2)
        {
            AnsiConsole.WriteLine();
            string hero1name = hero1 != null ? hero1.Name : " - ";
            string hero2name = hero2 != null ? hero2.Name : " - ";
            var table = new Table();
            table.AddColumn(hero1name);
            table.AddColumn("VS.");
            table.AddColumn(hero2name);
            //table.Expand();
            table.Centered();
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }

        internal static void CreateCenterBlinking(string v)
        {
            AnsiConsole.WriteLine();
            var table = new Table();
            table.AddColumn(v);
            table.Expand();
            table.Width(50);
            table.Centered();
            table.Columns[0].Centered();
            AnsiConsole.Write(table);
        }

        public static void ShowTableOfHeroes(List<HeroModel> heroes)
        {
            // Create a table
            var table = new Table();
            table.Border(TableBorder.Rounded);
            // Add some columns

            table.AddColumn("ID");
            table.AddColumn("Nimi");
            table.AddColumn(new TableColumn("Energia"));
            table.AddColumn(new TableColumn("Hyökkäys"));
            table.AddColumn(new TableColumn("Puolustus"));
            table.AddColumn(new TableColumn("Viive"));
            table.AddColumn(new TableColumn("Spesiaali esine"));

            heroes.ForEach(hero => {

                // Add rows
                table.AddRow(
                    $"[magenta]{heroes.IndexOf(hero)}[/]",
                    Truncate($"{hero.Name}", 40), 
                    $"[red]{Math.Round(hero.Stats.Health, 2)}[/]", 
                    $"[orange3]{Math.Round(hero.Stats.Attack, 2)}[/]", 
                    $"[blue]{Math.Round(hero.Stats.Defence, 2)}[/]", 
                    $"[purple]{Math.Round(hero.Stats.Delay, 2)}[/]",
                    $"[green]{hero.Speciality}[/]"
                 );
                
            });
           

            // Render the table to the console
            AnsiConsole.Write(table);
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : $"{value.Substring(0, maxLength)}...";
        }
    }
}
