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
        public static void CreateHeader()
        {
            AnsiConsole.Write(
                new FigletText("TAISTELEVAT PORKKANAT")
                    .LeftAligned()
                    .Color(Color.Red));
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

        public static void ShowTableOfHeroes(List<HeroModel> heroes)
        {
            // Create a table
            var table = new Table();
            table.Border(TableBorder.Rounded);
            // Add some columns

            table.AddColumn("Nimi");
            table.AddColumn(new TableColumn("Health"));
            table.AddColumn(new TableColumn("Attack"));
            table.AddColumn(new TableColumn("Defence"));
            table.AddColumn(new TableColumn("Delay"));

            heroes.ForEach(hero => {

                // Add rows
                table.AddRow(
                    Truncate($"{heroes.IndexOf(hero)}. {hero.Name}", 50), 
                    $"[red]{Math.Round(hero.Stats.Health, 2)}[/]", 
                    $"[orange3]{Math.Round(hero.Stats.Attack, 2)}[/]", 
                    $"[blue]{Math.Round(hero.Stats.Defence, 2)}[/]", 
                    $"[purple]{Math.Round(hero.Stats.Delay, 2)}[/]"
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
