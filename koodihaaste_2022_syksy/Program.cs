// See https://aka.ms/new-console-template for more information

using koodihaaste_2022_syksy;
using koodihaaste_2022_syksy.SpectreOutput;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

//SpectreUtils.CreateHeader();

//Console.WriteLine("Hello, World!");
//Console.Read(); // Console.ReadKey(true);); 
public class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        var configuration = builder.Build();

        var runner = new Runner();
        runner.Run();
    }
}