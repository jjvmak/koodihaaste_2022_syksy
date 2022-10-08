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
                new FigletText("RÄHINÄ PORKKANA")
                    .LeftAligned()
                    .Color(Color.Red));
        }
    }
}
