using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
